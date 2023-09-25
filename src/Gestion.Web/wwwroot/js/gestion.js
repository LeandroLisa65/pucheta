var doc;
doc = $(document);
doc.ready(StartGestion);

/*var avatar = new Avatar();*/
var imageAvatar = (evt) => {
    avatar.archivo(evt, "conten_Avatar");
};

var Loadin = {

    start: function (capa) {
        if (capa === undefined) {
            capa = "contenBody";
        }
        var ancho = $("#" + capa).width();
        var alto = $("#" + capa).height();
        var posicion = $("#" + capa).position();

        $("#load").css({ 'top': posicion.top, 'left': posicion.left, 'height': alto, 'width': ancho });
        $("#load").show();
    },

    stop: function () {
        $("#load").hide();
    }
};

function StartGestion() {

    $(document).ajaxStart(function () {
        Loadin.start();
    });
    
    $(document).ajaxStop(function () {
        Loadin.stop();
    });

    let url = window.location.pathname;
    
    if (url === "/Gestion/Perfil" || url === "/Gestion/Perfil/") {
        document.getElementById('FAvatar').addEventListener('change', imageAvatar, false);
        
    }


}

