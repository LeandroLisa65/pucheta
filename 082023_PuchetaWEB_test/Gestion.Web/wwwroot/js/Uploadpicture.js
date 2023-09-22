class Uploadpicture {
    constructor() {

    }
    archivo(evt, id) {
        let files = evt.target.files;
        let f = files[0];
        if (f.type.match('image.*')) {
            let reader = new FileReader();
            reader.onload = ((theFile) => {
                return (e) => {
                    document.getElementById(id).innerHTML = ['<img src="', e.target.result, '"title="', escape(theFile.name), '"/>'].join('');
                    document.getElementById("FAvatar_label").innerHTML = theFile.name;
                };
            })(f);
            reader.readAsDataURL(f);
        }
    }
}