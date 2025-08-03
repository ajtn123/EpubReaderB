function resizeIframe(iframe) {
    if (!iframe) return;

    function updateSize() {
        const html = iframe.contentDocument.documentElement;
        const height = html.getBoundingClientRect().height + 50;
        iframe.style.height = height + 'px';
    }

    updateSize();
    new ResizeObserver(updateSize).observe(iframe.contentDocument.body);
}


function injectLinkScript(iframe) {
    const doc = iframe.contentDocument;
    const script = doc.createElement('script');
    script.textContent = `
                  (function () {
                    document.querySelectorAll('a[href]').forEach((link) => {
                      link.addEventListener('click', function (e) {
                        e.preventDefault();
                        if (this.href.includes(location.origin)) {
                          let path = this.href.replace(location.origin + '/resources/', '');
                          let [key, a] = path.split('#');
                          window.parent.scrollToAnchor(key, a);
                        } else { window.parent.open(this.href, '_blank'); }
                      });
                    });
                  })();
                `;
    doc.head.appendChild(script);
}