// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const dropArea = document.getElementById('drop-area');

document.addEventListener('dragover', (e) => {
  e.preventDefault();
  dropArea.style.display = 'block';
});

document.addEventListener('dragleave', (e) => {
  dropArea.style.display = 'none';
});

document.addEventListener('drop', async (e) => {
  e.preventDefault();
  dropArea.style.display = 'none';

  const file = e.dataTransfer.files[0];
  if (!file) return;

  const formData = new FormData();
  formData.append('file', file);

  const response = await fetch('/Upload/UploadFile', {
    method: 'POST',
    body: formData,
  });

  const result = await response.json();

  if (result.success && result.redirect) {
    window.location.href = result.redirect;
  } else {
    alert(result.message || 'Upload failed');
  }
});


/*
* bootstrap-auto-dark-mode (modified)
*
* Author and copyright: Stefan Haack (https://shaack.com)
* Repository: https://github.com/shaack/bootstrap-auto-dark-mode)
* License: MIT, see file 'LICENSE'
*/

const htmlElement = document.querySelector('html');

function updateTheme() {
    const theme = window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light';
    document.querySelector("html").setAttribute('data-bs-theme', theme);
}

window.matchMedia('(prefers-color-scheme: dark)').addEventListener('change', updateTheme);
updateTheme();


/*
 * hide bootstrap navbar when scrolling (modified)
 *
 * Source - https://stackoverflow.com/a/45935816
 * Posted by Tomer Shay, modified by community. See post 'Timeline' for change history
 * Retrieved 2026-01-16, License - CC BY-SA 3.0
 */

const nav = document.querySelector("nav");
const bannerHeight = nav.offsetHeight;
let lastScrollTop = 0;

window.addEventListener("scroll", function () {
    const currScrollTop = window.pageYOffset || document.documentElement.scrollTop;

    if (currScrollTop < bannerHeight) {
        nav.style.transform = "translateY(0)";
    } else if (currScrollTop > lastScrollTop) {
        nav.style.transform = "translateY(-150%)";
    } else {
        nav.style.transform = "translateY(0)";
    }

    lastScrollTop = currScrollTop;
});