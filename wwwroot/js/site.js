// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const mask = document.getElementById('drag-mask');

document.addEventListener('dragover', e => {
    e.preventDefault();
    mask.style.display = 'flex';
});

document.addEventListener('dragleave', e => {
    mask.style.display = 'none';
});

document.addEventListener('drop', async e => {
    e.preventDefault();
    mask.style.display = 'none';

    const file = e.dataTransfer.files[0];
    if (!file) return;

    const formData = new FormData();
    formData.append('file', file);

    const response = await fetch('/Upload/UploadFile', {
        method: 'POST',
        body: formData
    });

    const result = await response.json();

    if (result.success && result.redirect) {
        window.location.href = result.redirect;
    } else {
        alert(result.message || "Upload failed");
    }
});
