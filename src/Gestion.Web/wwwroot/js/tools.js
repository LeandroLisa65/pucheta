

function validaDatos(v1, v2, v3, v4) {
    var Ok = false;
    var Mensaje = "";
    $('#' + v1).find('[data-val-required]').each(function (index) {
        if (this.type === "text" || this.type === "number") {
            if ($('#' + this.id).val() === "") {
                $('#' + this.id).removeClass("is-valid");
                $('#' + this.id).addClass("is-invalid");
                Mensaje = Mensaje + $('#' + this.id).attr('data-val-required') + "<br>";
                Ok = true;
            } else {
                var tieneRE = $('#' + this.id).attr('data-val-regex-pattern');
                if (tieneRE !== undefined) {
                    var gRE = new RegExp(tieneRE);
                    if (!gRE.test($('#' + this.id).val())) {
                        $('#' + this.id).removeClass("is-valid");
                        $('#' + this.id).addClass("is-invalid");
                        Mensaje = Mensaje + $('#' + this.id).attr('data-val-regex') + "<br>";
                        Ok = true;
                    } else {
                        $('#' + this.id).removeClass("is-invalid");
                        $('#' + this.id).addClass("is-valid");
                    }
                } else {
                    $('#' + this.id).removeClass("is-invalid");
                    $('#' + this.id).addClass("is-valid");
                }
            }
        }
    });
    var dReturn = {
        isError: Ok,
        mensaje: Mensaje
    };
    return dReturn;
}

function validaDatosV2(v1, v2, v3, v4) {
    var Ok = false;
    var Mensaje = "";
    $('#' + v1).find('[data-val-required], [maxlength]').each(function (index) {


        if (this.type === "text" || this.type === "number") {

            var mxL = $('#' + this.id).attr('maxlength');

            if ($('#' + this.id).val() === "" && mxL === undefined) {
                $('#' + this.id).removeClass("is-valid");
                $('#' + this.id).addClass("is-invalid");
                Mensaje = Mensaje + $('#' + this.id).attr('data-val-required') + "<br>";
                Ok = true;
            } else {
                
                if (mxL !== undefined) {
                    
                    if ($('#' + this.id).val().length > parseInt(mxL)) {
                        $('#' + this.id).removeClass("is-valid");
                        $('#' + this.id).addClass("is-invalid");
                        Mensaje = Mensaje + $('#' + this.id).attr('data-val-length') + "<br>";
                        Ok = true;
                    } else {
                        $('#' + this.id).removeClass("is-invalid");
                        $('#' + this.id).addClass("is-valid");
                    }
                } else {
                    $('#' + this.id).removeClass("is-invalid");
                    $('#' + this.id).addClass("is-valid");
                }
            }
        }
    });
    var dReturn = {
        isError: Ok,
        mensaje: Mensaje
    };
    return dReturn;
}

function evitaSaltoLinea(texto) {
    var Gtexto = texto.replace(/\n/g, "<br />");
    return Gtexto;
}
