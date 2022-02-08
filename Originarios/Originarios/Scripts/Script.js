/********************* Formatação máscara data nascimento *********************/
const formatDtNasc = () => {
    const hiddenDtNasc = document.getElementById('hiddenDtNasc').value.trim();
    if (hiddenDtNasc) {
        const day = hiddenDtNasc.substring(0, 2);
        const month = hiddenDtNasc.substring(3, 5);
        const year = hiddenDtNasc.substring(6, 10);
        document.getElementById('inputDate').value = `${year}-${month}-${day}`;
    }
}
window.addEventListener('load', formatDtNasc);

/********************* Alterar botões de 'salvar imagem' *********************/
const mudarBotao = (num, foiSalva) => {
    let btnCanvas = document.getElementById(`btnCanvas${num}`);
    if (foiSalva) {
        btnCanvas.className = "btn btn-success";
        btnCanvas.innerHTML = "Imagem Salva";
    } else {
        btnCanvas.className = "btn btn-danger";
        btnCanvas.innerHTML = "Salvar Imagem";
    }
}

/********************* Tratamento pré criação do produto *********************/
const preSubmit = () => {

    const titulo = document.getElementById('titulo');
    if (titulo.value.trim() === '') {
        alert("É necessário preencher um título.")
        titulo.focus();
        return;
    }
    const descricao = document.getElementById('descricao');
    if (descricao.value.trim() === '') {
        alert("É necessário preencher uma descrição.")
        descricao.focus();
        return;
    }

    document.getElementById('formSalvarPost').submit();
};
document.getElementById('btnSalvarProduto').addEventListener('click', preSubmit);

/********************* Códigos para cortar imagem *********************/
///////////////////// códigos gerais /////////////////////
let cropper1 = null;
let preCrop1 = document.getElementById('preCrop1');
let cropper2 = null;
let preCrop2 = document.getElementById('preCrop2');
let cropper3 = null;
let preCrop3 = document.getElementById('preCrop3');
let cropper4 = null;
let preCrop4 = document.getElementById('preCrop4');
const initCrops = () => {
    initCrop1();
    initCrop2();
    initCrop3();
    initCrop4();
};
window.onload = initCrops;

///////////////////// cropper imagem 1 /////////////////////
const initCrop1 = () => {
    'use strict';
    const options = {
        aspectRatio: 3 / 5,
        preview: '.posCrop1'
    };
    cropper1 = new Cropper(preCrop1, options);
};
const carregarImg1 = (event) => {
    cropper1.destroy();
    cropper1 = null;
    let reader = new FileReader();
    reader.onload = () => {
        preCrop1.src = reader.result;
        initCrop1();
        mudarBotao('1', false);
    }
    reader.readAsDataURL(event.target.files[0]);
};
const obterCanvas1 = () => {
    const canvas = cropper1.getCroppedCanvas({ maxWidth: 300, maxHeight: 500 });
    const urlcanvas = canvas.toDataURL('image/png');
    document.getElementById('hiddenCrop1').value = urlcanvas;
    mudarBotao('1', true);
}
document.getElementById('btnCanvas1').addEventListener('click', obterCanvas1);

///////////////////// cropper imagem 2 /////////////////////
const initCrop2 = () => {
    'use strict';
    const options = {
        aspectRatio: 3 / 5,
        preview: '.posCrop2'
    };
    cropper2 = new Cropper(preCrop2, options);
};
const carregarImg2 = (event) => {
    cropper2.destroy();
    cropper2 = null;
    let reader = new FileReader();
    reader.onload = () => {
        preCrop2.src = reader.result;
        initCrop2();
        mudarBotao('2', false);
    }
    reader.readAsDataURL(event.target.files[0]);
};
const obterCanvas2 = () => {
    const canvas = cropper2.getCroppedCanvas({ maxWidth: 300, maxHeight: 500 });
    const urlcanvas = canvas.toDataURL('image/png');
    document.getElementById('hiddenCrop2').value = urlcanvas;
    mudarBotao('2', true);
}
document.getElementById('btnCanvas2').addEventListener('click', obterCanvas2);


///////////////////// cropper imagem 3 /////////////////////
const initCrop3 = () => {
    'use strict';
    const options = {
        aspectRatio: 3 / 5,
        preview: '.posCrop3'
    };
    cropper3 = new Cropper(preCrop3, options);
};
const carregarImg3 = (event) => {
    cropper3.destroy();
    cropper3 = null;
    let reader = new FileReader();
    reader.onload = () => {
        preCrop3.src = reader.result;
        initCrop3();
        mudarBotao('3', false);
    }
    reader.readAsDataURL(event.target.files[0]);
};
const obterCanvas3 = () => {
    const canvas = cropper3.getCroppedCanvas({ maxWidth: 300, maxHeight: 500 });
    const urlcanvas = canvas.toDataURL('image/png');
    document.getElementById('hiddenCrop3').value = urlcanvas;
    mudarBotao('3', true);
}
document.getElementById('btnCanvas3').addEventListener('click', obterCanvas3);


///////////////////// cropper imagem 4 /////////////////////
const initCrop4 = () => {
    'use strict';
    const options = {
        aspectRatio: 3 / 5,
        preview: '.posCrop4'
    };
    cropper4 = new Cropper(preCrop4, options);
};
const carregarImg4 = (event) => {
    cropper4.destroy();
    cropper4 = null;
    let reader = new FileReader();
    reader.onload = () => {
        preCrop4.src = reader.result;
        initCrop4();
        mudarBotao('4', false);
    }
    reader.readAsDataURL(event.target.files[0]);
};
const obterCanvas4 = () => {
    const canvas = cropper4.getCroppedCanvas({ maxWidth: 300, maxHeight: 500 });
    const urlcanvas = canvas.toDataURL('image/png');
    document.getElementById('hiddenCrop4').value = urlcanvas;
    mudarBotao('4', true);
}
document.getElementById('btnCanvas4').addEventListener('click', obterCanvas4);