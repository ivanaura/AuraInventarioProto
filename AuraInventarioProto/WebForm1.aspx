<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="AuraInventarioProto.WebForm1" %>
<!DOCTYPE html>
<html>
<head>
    <base href="http://demos.telerik.com/kendo-ui/pdf-export/page-layout">
    <style>html { font-size: 14px; font-family: Arial, Helvetica, sans-serif; }</style>
    <title></title>
    <script src="http://code.jquery.com/jquery-1.12.3.min.js"></script>
    <script src="http://kendo.cdn.telerik.com/2017.1.118/js/jszip.min.js"></script>
    <script src="http://kendo.cdn.telerik.com/2017.1.118/js/kendo.all.min.js"></script>
    <script src="http://kendo.cdn.telerik.com/2017.1.118/js/angular.min.js"></script>
    <link href="~/Content/PageLayout.css" rel="stylesheet" />
</head>
<body>
<div id="example">
    <div class="box wide hidden-on-narrow">
        <div class="box-col">
            <h4>Get PDF</h4>
            <button class="export-pdf k-button" onclick="getPDF('.pdf-page')">Export</button>
        </div>
    </div>

    <div class="page-container hidden-on-narrow">
        <div class="pdf-page size-letter">
            <div class="pdf-header">
                <span class="company-logo">
                    <img src="../content/web/framework/company-logo.png" /> Blauer See Delikatessen
                </span>
                &nbsp;<span class="invoice-number">Invoice #23543</span></div>
            <div class="pdf-footer">
                <p>Blauer See Delikatessen<br />
                    Lützowplatz 456<br />
                    Berlin, Germany,  10785
                </p>
            </div>
            <div class="for">
                <h3>Invoice For</h3>
                <p>Antonio Moreno<br />
                    Naucalpan de Juárez<br />
                    México D.F., Mexico, 53500
                </p>
            </div>

            <div class="from">
                <h3>From</h3>
                <p style="padding-bottom: 20px; border-bottom: 1px solid #e5e5e5;">Hanna Moos <br />
                    Lützowplatz 456<br />
                    Berlin, Germany,  10785
                </p>
                <p style="padding-top: 20px;">
                    Invoice ID: 23543<br />
                   Invoice Date: 12.03.2014<br />
                   Due Date: 27.03.2014
                </p>
            </div>
            <div class="pdf-body">

                <p class="signature">
                    Signature: ________________ <br /><br />
                    Date: 12.03.2014
                </p>
            </div>
        </div>
    </div>
    
    <div class="responsive-message"></div>

    <style>
        /*
            Use the DejaVu Sans font for display and embedding in the PDF file.
            The standard PDF fonts have no support for Unicode characters.
        */
        .pdf-page {
            font-family: "DejaVu Sans", "Arial", sans-serif;
        }
    </style>

    <script>
        // Import DejaVu Sans font for embedding

        // NOTE: Only required if the Kendo UI stylesheets are loaded
        // from a different origin, e.g. cdn.kendostatic.com
        kendo.pdf.defineFont({
            "DejaVu Sans": "https://kendo.cdn.telerik.com/2016.2.607/styles/fonts/DejaVu/DejaVuSans.ttf",
            "DejaVu Sans|Bold": "https://kendo.cdn.telerik.com/2016.2.607/styles/fonts/DejaVu/DejaVuSans-Bold.ttf",
            "DejaVu Sans|Bold|Italic": "https://kendo.cdn.telerik.com/2016.2.607/styles/fonts/DejaVu/DejaVuSans-Oblique.ttf",
            "DejaVu Sans|Italic": "https://kendo.cdn.telerik.com/2016.2.607/styles/fonts/DejaVu/DejaVuSans-Oblique.ttf"
        });
    </script>

    <!-- Load Pako ZLIB library to enable PDF compression -->
    <script src="../content/shared/js/pako.min.js"></script>

    <script>
        function getPDF(selector) {
            kendo.drawing.drawDOM($(selector)).then(function (group) {
                kendo.drawing.pdf.saveAs(group, "Invoice.pdf");
            });
        }
    </script>
</div>

</body>
</html>
