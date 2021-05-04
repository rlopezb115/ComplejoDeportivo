let callbackConfirmacion = null;

function ModalMostraConfirmacion(mensaje, callback) {
    const confirmacion = document.getElementById('confirmacion-detalle');
    confirmacion.innerText = mensaje;
    callbackConfirmacion = callback;
    $('#modalConfirmacion').modal('show');
}

async function ModalConfirmarConfirmacion() {
    $('#modalConfirmacion').modal('hide');
    await callbackConfirmacion();
}

const ModalMostrarMensaje = (type = 'SUCCESS', mensaje) => {
    const icono = document.getElementById('mensaje-icono');
    switch (type) {
        case 'ERROR':
            icono.innerHTML = `
            <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-circle-x" width="80" height="80" viewBox="0 0 24 24" stroke-width="2.5" stroke="#ff2825" fill="none" stroke-linecap="round" stroke-linejoin="round">
                <path stroke="none" d="M0 0h24v24H0z" fill="none" />
                <circle cx="12" cy="12" r="9" />
                <path d="M10 10l4 4m0 -4l-4 4" />
            </svg>`
            break;

        case 'SUCCESS':
            icono.innerHTML = `
                <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-circle-check" width="80" height="80" viewBox="0 0 24 24" stroke-width="2.5" stroke="#7bc62d" fill="none" stroke-linecap="round" stroke-linejoin="round">
                    <path stroke="none" d="M0 0h24v24H0z" fill="none" />
                    <circle cx="12" cy="12" r="9" />
                    <path d="M9 12l2 2l4 -4" />
                </svg>`;
            break;
    }

    document.getElementById('mensaje-detalle').innerText = mensaje;
    $('#modalMansaje').modal('show');
}