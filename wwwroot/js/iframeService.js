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

function injectScript(iframe) {
    const doc = iframe.contentDocument;
    const script = doc.createElement('script');
    script.textContent = `
            document.querySelectorAll('a[href]').forEach((link) => {
              link.addEventListener('click', function (e) {
                e.preventDefault();
                if (this.href.includes(location.origin)) {
                  const path = this.href.replace(location.origin + '/resources/', '');
                  const [key, a] = path.split('#');
                  window.parent.scrollToAnchor(key, a);
                } else { window.parent.open(this.href, '_blank'); }
              });
            });

            const style = document.createElement('style');
            style.type = 'text/css';
            style.innerHTML = window.parent.injectedStyles;
            document.head.prepend(style);
            document.addEventListener('dragover', e => { e.preventDefault(); });
            document.addEventListener('drop', async e => { e.preventDefault(); });
        `;
    doc.head.appendChild(script);
}