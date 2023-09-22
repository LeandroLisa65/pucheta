function cargarSelect(id, lObjetos, campoValor, campoTexto, valorPorDefecto) {
    try {
        let slc = document.getElementById(id);
        if (slc) {
            let opciones = "";
            lObjetos.forEach(o => {
                const selected = valorPorDefecto == o[campoValor] ? "selected" : "";
                opciones += "<option value='" + o[campoValor] + "' " + selected + ">" + o[campoTexto] + "</option>";
            });
            slc.innerHTML = opciones;
        }
        else {
            console.log("No existe elemento con id: ${id}");
        }
    }
    catch (error) {
        console.log("Error: CargarSelect(id, lObjetos, campoValor, campoTexto, opcionPorDefecto)");
        console.log(error);
    }
}

function getStringFecha_YYYYMMDD(date) {
    if (date) {
        var mes = date.getMonth() + 1;
        var dia = date.getDate();
        var fechaHasta = date.getFullYear() + "-" + (mes < 10 ? ("0" + mes) : mes) + "-" + (dia < 10 ? ("0" + dia) : dia);
        return fechaHasta;
    }
    else return "0001-01-01";
}

function getNumeroFormateado(numero) {
    if (numero) {
        numero = parseFloat(numero);
        return numero.toFixed(2).toString().replace(".", ",").replace(/\B(?<!\.\d*)(?=(\d{3})+(?!\d))/g, ".");
    }
    else return "0,00";
}