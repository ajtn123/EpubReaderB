# EpubReaderB

Read EPUB books in any browser.

## Install

Download release and unzip.

### As default app

Right click on an EPUB file in explorer > Open with > More > Select other desktop application > EpubReaderB.exe > Always

## Usage

### Open server

If EpubReaderB is the default app for EPUB files, double click on any EPUB file in explorer.

Otherwise, you can use the following command, or simply start EpubReaderB.exe without a file.

```
.\EpubReaderB.exe E:\Path\to\epub\file
```

### Connect to server

Open `http://localhost:5000` in any browser, you should be able to see the book, or untitled page if you started without a book.

If you allowed EpubReaderB.exe to pass the firewall, you can access the book in browsers on other devices.

```
http://<Host IP>:5000
```

As this only supports http, aviod usage in public networks.

Drop an EPUB file onto the browser tab to switch book.
