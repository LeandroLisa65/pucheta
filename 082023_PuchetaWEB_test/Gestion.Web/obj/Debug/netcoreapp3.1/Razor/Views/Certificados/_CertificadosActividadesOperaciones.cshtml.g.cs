#pragma checksum "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/Certificados/_CertificadosActividadesOperaciones.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d5da110b8850298b697c438be22273b89f439fac"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Certificados__CertificadosActividadesOperaciones), @"mvc.1.0.view", @"/Views/Certificados/_CertificadosActividadesOperaciones.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 2 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/_ViewImports.cshtml"
using Gestion.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/_ViewImports.cshtml"
using Gestion.Web.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/_ViewImports.cshtml"
using Gestion.Web.ViewModels.Input;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d5da110b8850298b697c438be22273b89f439fac", @"/Views/Certificados/_CertificadosActividadesOperaciones.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3bee31758dd1cbb7e600e265aed2cb74227e9428", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Certificados__CertificadosActividadesOperaciones : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Gestion.EF.ReturnData>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/plugins/kendoui_2022_2_802/styles/kendo.bootstrap.min.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/plugins/kendoui_2022_2_802/js/jquery.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/plugins/kendoui_2022_2_802/js/kendo.all.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "d5da110b8850298b697c438be22273b89f439fac5017", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"



<div class=""row"">
    <div class=""col-lg-12"">
     
        <div class=""card  card-rojo"">
            <div class=""row"">
                <div class=""col-12"" style=""padding-top: 2.3%; padding-left: 2%;"">
                    <div style=""text-align: right;padding-right:2%"">
                        <button class=""btn btn-primary"" type=""button"" onclick=""Actualizar()"">Refrescar</button>
                    </div>
                </div>
            </div>
            <div class=""card-body"">
                <div id=""GrillaKendoActividad2""></div>
            </div>
                
        </div>

        
    </div>
</div>

<p></p>
  
");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d5da110b8850298b697c438be22273b89f439fac6802", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d5da110b8850298b697c438be22273b89f439fac7827", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"

<style>
    /*Estilo para el boton editar de la Grilla*/
    .k-icon.k-i-refresh {
        color: #1367c1;  /*  #00BFFF; Celeste */
    }
</style>
<script type=""text/x-kendo-template"" id=""Grid-Btn-template3"">
    
    <div>  #= act_Esta_Liquidado ? '<img width=""50"" height=""30"" src=""../dist/img/pagado.png""/>' : act_Presenta_Liquidacion ? '<img src=""../dist/img/checkbox.png"" />' : '<img src=""../dist/img/uncheckbox.png"" />' # </div>

</script>
<script>
  
    //var dataGlobalCertificados = JSON.parse('Html.Raw(Json.Serialize(Model))');
    var dataGlobalCertificados = JSON.parse(JSON.stringify(");
#nullable restore
#line 47 "/Users/leandrolisa/Projects/082023_PuchetaWEB_test/Gestion.Web/Views/Certificados/_CertificadosActividadesOperaciones.cshtml"
                                                      Write(Html.Raw(Json.Serialize(Model)));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"));
    var lstActividades= dataGlobalCertificados.data3;
   
    _cargarGrilla(lstActividades);

    //#region DEFINICIONES-GENERALES


    $('#chkCerRubro').kendoCheckBox({
        label: ""Rubro""
    });
    $('#chkCerMedida').kendoCheckBox({
        label: ""Unidad Medida""
    });
    $('#chkCerPresupuestado').kendoCheckBox({
        label: ""Presupuestado""
    });
    $('#chkCerMonto').kendoCheckBox({
        label: ""Monto""
    });

    var dataSource = null;
    //#endregion
 
    //#region DEFINICIONES-GRILLA
    function _cargarGrilla(data) {
        if (dataGlobalCertificados.data2 == ""Lectura"" || dataGlobalCertificados.data2 == ""A Pagar"") {
            dataSource = new kendo.data.DataSource({
                autoSync: false,
                pageSize: 50,
                data: data,
                batch: true,
                aggregate: [
                    { field: ""act_Acum_Ant_Moneda"", aggregate: ""sum"", format: ""{0:N2}"" },
                    { field: ""act_Cert_Act");
            WriteLiteral(@"ual_Moneda"", aggregate: ""sum"", format: ""{0:N2}"" },
                    { field: ""act_Acum_Actual_Moneda"", aggregate: ""sum"", format: ""{0:N2}"" },
                    { field: ""monto_Total"", aggregate: ""sum"", format: ""{0:N2}"" },
                    { field: ""cantidad_Asignada"", aggregate: ""sum"", format: ""{0:N2}"" }
                ],
                schema: {

                    model: {
                        id: ""id"",
                        fields: {
                            id: { editable: false, nullable: false },
                            nroNotaPedido: { editable: false, type: ""string"", nullable: false },
                            ubicacionNombre: { editable: false, nullable: false },
                            rubroNombre: { editable: false, nullable: false },
                            actividadNombre: { editable: false, nullable: false },
                            detalle: { editable: false, nullable: false },
                            partidaCodigo: { editable: false, nul");
            WriteLiteral(@"lable: false },
                            actividadNombre: { editable: false, nullable: false },
                            //-
                            cantidad_Asignada: { editable: false, nullable: false },
                            cantidad_Asignada_UniMedida: { editable: false, nullable: false },
                            monto_Unitario: { editable: false, nullable: false },
                            monto_Total: { editable: false, nullable: false },
                            monto_Total_Generico: { editable: false, nullable: false }, //para esconderlo
                            //-
                            act_Presenta_Liquidacion: { editable: false, type: ""boolean"" },
                            //-
                            act_Acum_Ant_Unid: { editable: false, type: ""number"", nullable: false },
                            vis_Liquida_FDesde: { editable: false, nullable: false },
                            vis_Liquida_FHasta: { editable: false, nullable: false },
   ");
            WriteLiteral(@"                         act_Avance_Periodo_Unid: { editable: false, type: ""number"", nullable: false, validation: { min: 0 } },
                            act_Acum_Total_Unid: { editable: false, nullable: false },
                            //-
                            act_Acum_Ant_Moneda: { editable: false, nullable: false },
                            act_Cert_Actual_Moneda: { editable: false, nullable: false },
                            act_Acum_Actual_Moneda: { editable: false, nullable: false },
                            //-
                            act_Esta_Liquidado: { editable: false, type: ""boolean"" }
                            //--
                        }
                    }
                }
            });
        }else{
            dataSource = new kendo.data.DataSource({
                autoSync: false,
                pageSize: 50,
                data: data,
                batch: true,
                aggregate: [
                    { field: ""act_Acum_An");
            WriteLiteral(@"t_Moneda"", aggregate: ""sum"", format: ""{0:N2}"" },
                    { field: ""act_Cert_Actual_Moneda"", aggregate: ""sum"", format: ""{0:N2}"" },
                    { field: ""act_Acum_Actual_Moneda"", aggregate: ""sum"", format: ""{0:N2}"" },
                    { field: ""monto_Total"", aggregate: ""sum"", format: ""{0:N2}"" },
                    { field: ""cantidad_Asignada"", aggregate: ""sum"" }
                ],
                schema: {

                    model: {
                        id: ""id"",
                        fields: {
                            id: { editable: false, nullable: false },
                            nroNotaPedido: { editable: false, type: ""string"", nullable: false },
                            ubicacionNombre: { editable: false, nullable: false },
                            rubroNombre: { editable: false, nullable: false },
                            actividadNombre: { editable: false, nullable: false },
                            detalle: { editable: false, nullable: f");
            WriteLiteral(@"alse },
                            partidaCodigo: { editable: false, nullable: false },
                            actividadNombre: { editable: false, nullable: false },
                            //-
                            cantidad_Asignada: { editable: false, nullable: false },
                            cantidad_Asignada_UniMedida: { editable: false, nullable: false },
                            monto_Unitario: { editable: false, nullable: false },
                            monto_Total: { editable: false, nullable: false },
                            monto_Total_Generico: { editable: false, nullable: false }, //para esconderlo
                            //-
                            act_Presenta_Liquidacion: { type: ""boolean"" },
                            //-
                            act_Acum_Ant_Unid: { editable: false, type: ""number"", nullable: false },
                            vis_Liquida_FDesde: { editable: false, nullable: false },
                            vis_L");
            WriteLiteral(@"iquida_FHasta: { editable: false, nullable: false },
                            act_Avance_Periodo_Unid: { editable: true, type: ""number"", nullable: false, validation: { min: 0 } },
                            //validation: { required: true, min: 0 }
                            act_Acum_Total_Unid: { editable: false, nullable: false },
                            //-
                            act_Acum_Ant_Moneda: { editable: false, nullable: false },
                            act_Cert_Actual_Moneda: { editable: false, nullable: false },
                            act_Acum_Actual_Moneda: { editable: false, nullable: false },
                            //-
                            act_Esta_Liquidado: { editable: false, type: ""boolean"" }
                            //--
                        }
                    }
                }
            });
        }
    
       

            $(""#GrillaKendoActividad2"").html(""<div id='grillaActividad'></div>"");

            var colGrilla=");
            WriteLiteral(@"[
            {
                title: ""<span style='color: white; font-weight: bold;'>ACTIVIDADES PLANIFICADAS Y PRESUPUESTADAS</span>"",
                headerAttributes: { style: ""background-color: green"" },
                columns:[
                        {
                        title: """",
                            columns:[
                            { field: ""nroNotaPedido"", title: ""N.P."", width: ""5px"" },
                            { field: ""ubicacionNombre"", title: ""Ubicación"", width: ""4px""},
                            { title: ""<span style='color: black;'>  Rubro y Actividad  </span>"", width: ""7.5px"", template: ""R: #=  data.rubroNombre #  </br> A: #=data.actividadNombre#""},
                            { field: ""detalle"", title: ""Comentario"", width: ""4.5px"" },
                            { field: ""partidaCodigo"", title: ""Partida"", width: ""3px"", attributes: { style: ""text-align:center;"" } }
                             ]
                        },
                        {
       ");
            WriteLiteral(@"               
                            title: ""<span style='color: black; font-weight: bold;'>Presupuesto <br/> a Realizar</span>"",
                        headerAttributes: { style: ""background-color:#05bf05;"" },
                            columns: [
                           
                                {
                                    title: """",
                                    field: ""monto_Total"",
                                    aggregates: [""sum""], footerTemplate: ""<div>$ #=sum#</div>"",
                                    headerAttributes: { style: ""text-align:center;"" },
                                    width: ""7px"",
                                    template: ""Cantidad: #=  data.cantidad_Asignada_UniMedida # </br> M. Unit: $#=data.monto_Unitario # </br> M. Total: $#=data.monto_Total#"",
                                    
                           
                                }
                            ]                           
                   ");
            WriteLiteral(@"     },
                        {
                       title: ""<span style='color: black; font-weight: bold;'>Liquida</span>"",
                        //headerAttributes: { style:  },
                            columns:[
                            { 
                                field: ""act_Presenta_Liquidacion"", 
                                filterable: false,
                                editable: function (dataItem) { return isEditable(dataItem);},
                                title: ""<span style='background-color: #129c12; font-weight: bold;'></span>"", 
                                template: kendo.template($(""#Grid-Btn-template3"").html()), 
                                width: ""5%"", media: ""(min-width: 4.5px)"", 
                                attributes: { style: ""text-align:center; background-color:grey;font-weight: bold;"" }
                            }
                            ]
                        },
                        {
                        title:");
            WriteLiteral(@" ""<span style='color: black; font-weight: bold;'>Avance Fisico</span>"",
                        headerAttributes: { style: ""background-color: #2ddf2d;text-align:center;"" },
                            columns:[
                            { field: ""act_Acum_Ant_Unid"", title: ""Acum. </br> Anterior"", filterable: false, width: ""4px"", attributes: { style: ""text-align:center;"" } },
                            {
                                title: ""Fecha"",
                                headerAttributes: { style: ""text-align:center;"" },
                                width: ""4.8px"", 
                                template: ""FD: #= data.vis_Liquida_FDesde == null? ' --/--/-- ' : data.vis_Liquida_FDesde # </br> FH: #= data.vis_Liquida_FHasta == null? ' --/--/-- ' : data.vis_Liquida_FHasta #"",
                            },                    
                            { 
                                field: ""act_Avance_Periodo_Unid"", 
                                title: ""Avance </br> Periodo");
            WriteLiteral(@""", 
                                editable: function (dataItem) { return isEditable(dataItem); },
                                filterable: false, width: ""4px"", 
                                attributes: { style: ""text-align:center;background-color:grey;font-weight: bold;"" }
                            },
                            { field: ""act_Acum_Total_Unid"", title: ""Acum. </br> Actual"", filterable: false, width: ""4%"", media: ""(min-width: 4px)"", attributes: { style: ""text-align:center;"" } }
                            ]
                        },
                        {
                            title: ""<span style='color: black; font-weight: bold;'>Importe</span>"",
                        headerAttributes: { style: ""background-color: #9bfb92;"" },
                            columns:[
                            { field: ""act_Acum_Ant_Moneda"", title: ""Acum. </br> Anterior"", filterable: false, format: ""{0:c}"", width: ""4px"", attributes: { style: ""text-align:center;"" }, aggregates: [""s");
            WriteLiteral(@"um""], footerTemplate: ""<div>$ #=sum#</div>"" },
                            { field: ""act_Cert_Actual_Moneda"", title: ""Certificado </br> Actual"", filterable: false, format: ""{0:c}"", width: ""4px"", attributes: { style: ""text-align:center;"" }, aggregates: [""sum""], footerTemplate: ""<div>$ #=sum#</div>"" },
                            { field: ""act_Acum_Actual_Moneda"", title: ""Acum. </br> Actual"", filterable: false, format: ""{0:c}"", width: ""5px"", attributes: { style: ""text-align:center;"" }, aggregates: [""sum""], footerTemplate: ""<div>$ #=sum#</div>"" }
                            ]
                        },
                    
                    //{ 
                    //    command: 
                    //        {
                    //        name: ""kcertificate"",
                    //        text: """",
                    //        click: grabarDetalleActividades
                          

                    //    },
                       
                    //     title: """", 
            ");
            WriteLiteral(@"        //     width: ""4%"", 
                    //     attributes: { style: ""text-align:center;"" } 
                    //}
                         {
                        command:
                        {
                            name: ""kcertificate"",
                            text: """",
                            click: grabarDetalleActividades


                        },

                        title: """",
                        width: ""4%"",
                        attributes: { style: ""text-align:center;"" }
                    }

               
                ]
            }
        ]

        /*
        
        
        */

            var Grilla= $(""#grillaActividad"").kendoGrid({        
                columns: colGrilla,
                navigatable: true,
            filterable: false,
                pageable: {
                    refresh: true,
                    pageSizes: true,
                    buttonCount: 20,
                },
           ");
            WriteLiteral(@"     dataSource: dataSource,
                editable: true,
                scrollable: true,
            height: _calcularAlturaGrilla(),
                reorderable: true,
                sortable: true,
                dataBound: function () {
                    dataView = this.dataSource.view();
                    var MostrarColumna= false;
                var grid = $(""#grillaActividad"").data(""kendoGrid"");
                    for (var i = 0; i < dataView.length; i++) {
                        var uid = dataView[i].uid;
                    //if (dataView[i].act_Presenta_Liquidacion) {
                    //        MostrarColumna=true;              
                    //    }

                    
                    }
                }
            }).data(""kendoGrid"");
}
    //#endregion

    //#region INTERFAZ-GRILLA

    $('#chkCerRubro').change(function (evento) {
        if ($('#chkCerRubro').prop('checked')) {
               // Grilla.hideColumn(""rubroNombre"");
       ");
            WriteLiteral(@"     }else{
                //Grilla.showColumn(""rubroNombre"");
            }
        });
    $('#chkCerMedida').change(function (evento) {
        if ($('#chkCerMedida').prop('checked')) {
            //Grilla.hideColumn(""unidad_Medida"");
            }else{
           // Grilla.showColumn(""unidad_Medida"");
            }
        });
    $('#chkCerMonto').change(function (evento) {
        if ($('#chkCerMonto').prop('checked')) {
           
            Grilla.hideColumn(""act_Acum_Ant_Moneda"");
            Grilla.hideColumn(""act_Cert_Actual_Moneda"");
            Grilla.hideColumn(""act_Acum_Actual_Moneda"");
            }else{
            Grilla.showColumn(""act_Acum_Ant_Moneda"");
            Grilla.showColumn(""act_Cert_Actual_Moneda"");
            Grilla.showColumn(""act_Acum_Actual_Moneda"");
            }
        });


    function isEditable(dataItem) {
      var valor = true;
        if (dataGlobalCertificados.data2 == ""Lectura"" || dataGlobalCertificados.data2 == ""A Pagar"") {
      ");
            WriteLiteral(@"          valor = false;
            }
            else{
                if (dataItem.Esta_Liquidado) {
                    valor = false;
                }
            }
       
        return valor;
}

    function grabarDetalleActividades(e){
    
        if (dataGlobalCertificados.data2 == ""Lectura"") {
            toastr.error(""No se puede editar en modo lectura."", { timeOut: 3000 });
            return;
        }
        if (dataGlobalCertificados.data2 == ""A Pagar"") {
            toastr.error(""No se puede editar cuando el certificado esta Pendiente de Pagar."", { timeOut: 3000 });
            return;
        }
    
        var d = this.dataItem($(e.currentTarget).closest(""tr""));
       
        var model = {

            id: d.id,
            act_Presenta_Liquidacion: d.act_Presenta_Liquidacion,
            act_Avance_Periodo_Unid: d.act_Avance_Periodo_Unid
        };

        if (d.act_Presenta_Liquidacion && (d.act_Avance_Periodo_Unid == null || d.act_Avance_Periodo_Unid ");
            WriteLiteral(@"== 0)) {
            toastr.error('No se cargo ningun avance.', { timeOut: 2000 });
            return;
        }
       
        $.post(""/Certificados/Update_Actividad"", model).done(function (e) {
            if (!e.isError) {
                toastr.success(e.data, { timeOut: 2000 });
                if(e.data2 != """"){
                    toastr.error(e.data2, { timeOut: 2000 });
                }
                 _cargarGrilla(e.data3);
            } else {
                toastr.error(e.data, { timeOut: 2000 });
                   _cargarGrilla(lstActividades);
            }
        });
}
   
    function _calcularAlturaGrilla(){
    let Altura =""500px"";

        if (lstActividades.length > 5){
            let Cantidad =(lstActividades.length) * 110;
            Altura= Cantidad.toString()+""px"";
        }
    return Altura;
}

    //#endregion


    function Actualizar() {
        var idCertificado = dataGlobalCertificados.data1;
        if (dataGlobalCertificados.data2 ==");
            WriteLiteral(@" ""Lectura"" || dataGlobalCertificados.data2 == ""A Pagar"") {
            toastr.error(""El certificado no se puede editar o actualizar por el Estado en el que se encuentra."", { timeOut: 3000 });
        } else {
            cargoActividades(idCertificado);
            toastr.success('Se actualizó la grilla correctamente.', { timeOut: 1500 });
        }
        
    }


</script>
");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Gestion.EF.ReturnData> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
