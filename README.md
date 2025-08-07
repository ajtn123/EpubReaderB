# EpubReaderB

[简体中文 zh-CN](#使用)

Read EPUB books in any browser.

## Install

Download release and unzip.

### As default app

Right click on an EPUB file in explorer > Open with > More > Select other desktop application > EpubReaderB.exe > Always

## Usage

### Start server

If EpubReaderB is the default app for EPUB files, double click on any EPUB file in explorer.

Otherwise, you can use the following command, or simply start EpubReaderB.exe without a file.

```
.\EpubReaderB.exe E:\Path\to\epub\file
```

### Connect to server

Open one of the prompted link, i.e. `http://localhost:5000`, in browser. You should be able to see the book, or an untitled page if you started without a book.

If you allowed EpubReaderB.exe to pass the firewall, you can access the book in browsers on other devices.

Aviod usage in public networks.

Drop an EPUB file onto the browser tab to switch book.

---

EPUB 阅读器

## 使用

下载 release 并解压，双击打开 EpubReaderB.exe。

浏览器访问任意提示的链接，例如：`http://localhost:5000`

如果添加了防火墙规则，则可以在局域网，甚至广域网（不推荐）内，的任意其他设备的浏览器访问，注意不要使用 `localhost`。

将 EPUB 文件拖动到打开的浏览器标签页即可开始阅读。在任意设备打开一个 EPUB，就可以在其他设备同步显示。
