const host = "http://localhost:5001";

////////////////////////////////////////////////////////////////////////////////////
///////    EndPoints
////////////////////////////////////////////////////////////////////////////////////

const EndPointDataTableComplejo = {
    url: `${host}/api/complejo`,
    headers: {
        "Content-Type": "application/json",
        "IncluirHATEOAS": "Y"
    },
    metodo: 'GET'
}

const EndPointRegistrarComplego = {
    url: `${host}/api/complejo`,
    headers: {
        "Content-Type": "application/json",
    },
    metodo: 'POST'
}

const EndPointSedes = {
    url: `${host}/api/sede`,
    headers: {
        'Content-Type': 'application/json'
    },
    metodo: 'GET'
}

const EndPointJefes = {
    url: `${host}/api/jefe`,
    headers: {
        'Content-Type': 'application/json'
    },
    metodo: 'GET'
}

const EndPointTiposComplejos = {
    url: `${host}/api/tipocomplejo`,
    metodo: 'GET',
    headers: {
        'Content-Type': 'application/json'
    }
}

// HTTP CLIENTE CATALOGOS
const obtenerSedesAsync = async () => {
    const { headers, metodo, url } = EndPointSedes;
    const response = await fetch(url, {
        method: metodo,
        headers: new Headers(headers),
    });
    console.log(response);
    const data = await response.json();
    return data;
}

const obtenerJefesAsync = async () => {
    const { headers, metodo, url } = EndPointJefes;

    const response = await fetch(url, {
        method: metodo,
        headers: new Headers(headers),
    });

    const data = await response.json();
    return data;
}

const obtenerTiposComplejosAsync = async () => {
    const { headers, metodo, url } = EndPointTiposComplejos;
    const response = await fetch(url, {
        method: metodo,
        headers: new Headers(headers),
    });

    const data = await response.json();
    return data;
}

// HTTP CLIENTE COMPLEJO
const registrarComplejo = async (url, metodo, data) => {
    let response = {};
    const headers = {
        'Content-Type': 'application/json',
    };
    console.log(data);

    try {
        const request = await fetch(url, {
            "method": metodo,
            "headers": new Headers(headers),
            "body": JSON.stringify(data)
        });

        response = await request.json();
    }
    catch (error) {
        console.log(error);
    }

    return response;
}

const actualizarComplejo = async (url, metodo, data) => {
    let response = {};
    const headers = {
        'Content-Type': 'application/json'
    };

    try {
        const request = await fetch(url, {
            "method": metodo,
            "headers": new Headers(headers),
            "body": JSON.stringify(data)
        });

        response = await request.json();
    }
    catch (error) {
        console.log(error);
    }

    return response;
}

const eliminarComplejo = async (url, metodo) => {
    let response = {};
    const headers = { 'Content-Type': 'application/json' };
    try {
        const request = await fetch(url, {
            "method": metodo,
            "headers": headers
        });

        response = await request.json();
    }
    catch (error) {
        console.log(error);
    }

    return response;
}

const detalleComplejo = async (url, metodo) => {
    let response = {};
    const headers = { 'Content-Type': 'application/json' };
    try {
        const request = await fetch(url, {
            "method": metodo,
            "headers": headers
        });

        response = await request.json();
    }
    catch (error) {
        console.log(error);
    }

    return response;
}

// EVENTO CATALOGOS
const _construirCatalogoJefes = jefes => {
    const elemento = document.getElementById('jefeId');
    jefes.forEach(jefe => {
        const opcion = document.createElement('OPTION');
        opcion.value = jefe.jefeId;
        opcion.innerText = jefe.nombre;
        elemento.appendChild(opcion);
    });
}

const _construirCatalogoSedes = sedes => {
    const elemento = document.getElementById('sedeId');
    sedes.forEach(sede => {
        const opcion = document.createElement('OPTION');
        opcion.value = sede.sedeId;
        opcion.innerText = sede.nombre;
        elemento.appendChild(opcion);
    });
}

const _construirCatalogoTiposComplejos = tiposComplejos => {
    const elemento = document.getElementById('tipoComplejoId');
    tiposComplejos.forEach(tipoComplejo => {
        const opcion = document.createElement('OPTION');
        opcion.value = tipoComplejo.tipoComplejoId;
        opcion.innerText = tipoComplejo.nombre;
        elemento.appendChild(opcion);
    });
}

const cargarCatalogos = async () => {
    const respuesta = await Promise.all([
        obtenerJefesAsync(),
        obtenerSedesAsync(),
        obtenerTiposComplejosAsync(),
    ]);
    _construirCatalogoJefes(respuesta[0]);
    _construirCatalogoSedes(respuesta[1]);
    _construirCatalogoTiposComplejos(respuesta[2]);
}

// EVENTO COMPLEJO
let _valoresCampos = {};
let _httpCliente = null;

const _resetearData = data => {
    if (Object.keys(data).length === 0) {
        _valoresCampos =
        {
            sedeId: 1,
            tipoComplejoId: 1,
            jefeId: 1,
            complejo: '',
            localizacion: '',
            noArea: 0,
            estado: true
        };
    }
    else {
        _valoresCampos =
        {
            sedeId: data.sedeId,
            tipoComplejoId: data.tipoComplejoId,
            jefeId: data.jefeId,
            complejo: data.complejo,
            localizacion: data.localizacion,
            noArea: data.noArea,
            estado: data.estado
        };
    }
    return _valoresCampos;
}

const _cambiarValoresEntrada = (data) => {
    document.getElementById('sedeId').value = data.sedeId;
    document.getElementById('tipoComplejoId').value = data.tipoComplejoId;
    document.getElementById('jefeId').value = data.jefeId;
    document.getElementById('complejo').value = data.complejo;
    document.getElementById('localizacion').value = data.localizacion;
    document.getElementById('noArea').value = data.noArea;
    document.getElementById('estado').checked = data.estado;
}

// eventos
const eventoCambioEntrada = e => {
    e.preventDefault();
    const { type } = e.target;
    switch (type) {
        case 'checkbox':
            _valoresCampos[e.target.name] = e.target.checked;
            break;
        default:
            const value = parseInt(e.target.value);
            if (isNaN(value)) {
                _valoresCampos[e.target.name] = e.target.value;
            } else {
                _valoresCampos[e.target.name] = value;
            }
            break;
    }
}

const eventoModalActualizarComplejo = async (url, metodo, id) => {
    const response = await detalleComplejo(url, 'GET');
    const data = _resetearData(response);
    _cambiarValoresEntrada(data);
    _httpCliente = { url, metodo };
    $('#modalFormulario').modal('show');
}

const eventoModalRegistrarComplejo = e => {
    e.preventDefault();
    const data = _resetearData({});
    _cambiarValoresEntrada(data);
    _httpCliente = EndPointRegistrarComplego;
    $('#modalFormulario').modal('show');
}

const eventSubmit = async e => {
    e.preventDefault();
    if (_valoresCampos.complejo === '') {
        console.log('El camplo complejo es requerido.');
    }
    _valoresCampos.noArea = _valoresCampos.noArea === '' ? 0 : _valoresCampos.noArea;
    const { metodo, url } = _httpCliente;
    switch (_httpCliente.metodo) {
        case "POST":
            const respuestaRegistrado = await registrarComplejo(url, metodo, _valoresCampos);
            if (respuestaRegistrado.complejoId > 0) {
                $('#dataTable').DataTable().clear().draw();
                $('#modalFormulario').modal('hide');
            }
            break;
        case "PUT":
            const respuestaActualizado = await actualizarComplejo(url, metodo, _valoresCampos);
            if (respuestaActualizado) {
                $('#dataTable').DataTable().clear().draw();
                $('#modalFormulario').modal('hide');
            }
    }
}

const CrearElementoDetalle = (campo, valor) => {
    const row = document.createElement('DIV');
    row.classList.add('row');

    // campo
    const columna1 = document.createElement('DIV');
    columna1.classList.add('col-4');

    const parrafoCampo = document.createElement('P');
    parrafoCampo.classList.add('font-weight-bold');
    parrafoCampo.innerText = campo;

    columna1.appendChild(parrafoCampo);

    // valor
    const columna2 = document.createElement('DIV');
    columna2.classList.add('col-8');

    const parrafoValue = document.createElement('P');
    parrafoValue.innerText = valor;

    columna2.appendChild(parrafoValue);

    row.appendChild(columna1);
    row.appendChild(columna2);

    return row;
}

const eventoEliminarComplejo = async (url, metodo) => {
    ModalMostraConfirmacion('¿Continuar con la eliminaciòn?', async function () {
        const data = await eliminarComplejo(url, metodo);
        $('#dataTable').DataTable().clear().draw();
        $('#modalFormulario').modal('hide');
    });
}

const eventoDetalleComplejo = async (url, metodo) => {
    const data = await detalleComplejo(url, metodo);
    const { complejoId, nombreSede, nombreTipoComplejo, nombreJefe, complejo, localizacion, noArea, estado } = data;
    const elemento = document.getElementById('detalleComplejo');

    const elementoComplejoId = CrearElementoDetalle("ID:", complejoId);
    const elementoSede = CrearElementoDetalle("Sede:", nombreSede);
    const elementoTipoComplejo = CrearElementoDetalle("Tipo Complejo:", nombreTipoComplejo);
    const elementoJefe = CrearElementoDetalle("Jefe:", nombreJefe);
    const elementoComplejo = CrearElementoDetalle("Complejo:", complejo);
    const elementoLocalizacion = CrearElementoDetalle("Localizacion:", localizacion);
    const elementoNoArea = CrearElementoDetalle("No. Area:", noArea);
    const elementoEstado = CrearElementoDetalle("Estado:", estado ? 'Activo' : 'Inactivo');

    elemento.innerHTML = '';
    elemento.appendChild(elementoComplejoId);
    elemento.appendChild(elementoSede);
    elemento.appendChild(elementoTipoComplejo);
    elemento.appendChild(elementoJefe);
    elemento.appendChild(elementoComplejo);
    elemento.appendChild(elementoLocalizacion);
    elemento.appendChild(elementoNoArea);
    elemento.appendChild(elementoEstado);

    // elemento 
    $('#ModalDetalleComplejo').modal('show');
}

const addEventInputsForm = () => {
    const InputSede = document.getElementById('sedeId');
    InputSede.addEventListener('change', eventoCambioEntrada);

    const InputTipoComplejo = document.getElementById('tipoComplejoId');
    InputTipoComplejo.addEventListener('change', eventoCambioEntrada);

    const InputJefe = document.getElementById('jefeId');
    InputJefe.addEventListener('change', eventoCambioEntrada);

    const InputComplejo = document.getElementById('complejo');
    InputComplejo.addEventListener('change', eventoCambioEntrada);

    const InputLocalizacion = document.getElementById('localizacion');
    InputLocalizacion.addEventListener('change', eventoCambioEntrada);

    const InputNoArea = document.getElementById('noArea');
    InputNoArea.addEventListener('change', eventoCambioEntrada);

    const InputEstado = document.getElementById('estado');
    InputEstado.addEventListener('change', eventoCambioEntrada);

    const submit = document.getElementById('modalFormulario');
    submit.addEventListener('submit', eventSubmit);

    const enlaceRegistrar = document.getElementById('registrar');
    enlaceRegistrar.addEventListener('click', eventoModalRegistrarComplejo);
    console.log(enlaceRegistrar.onclick);
}

////////////////////////////////////////////////////////////////////////////////
// DATATABLE
////////////////////////////////////////////////////////////////////////////////

const _administradorEventosGrupoEnlace = e => {
    const { url, metodo, id } = e.dataset;

    switch (metodo) {
        case 'PUT':
            eventoModalActualizarComplejo(url, metodo, id);
            break;
        case 'GET':
            eventoDetalleComplejo(url, metodo);
            break;
        case 'DELETE':
            eventoEliminarComplejo(url, metodo);
            break;
    }
};

const _construirGrupoEnlaces = enlaces => {
    let grupoBotones = '<nav class="navbar navbar-light bg-light">';
    enlaces.forEach(enlace => {
        switch (enlace.rel) {
            case 'actualizar':
                grupoBotones += `<button type="button" class="navbar-brand btn btn-warning data-table-enlace" data-url="${enlace.href}" data-metodo="${enlace.metodo}" data-id="${enlace.id}" onclick="_administradorEventosGrupoEnlace(this)">
                                    <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-tools" width="32" height="32" viewBox="0 0 24 24" stroke-width="2.5" stroke="#ffffff" fill="none" stroke-linecap="round" stroke-linejoin="round">
                                      <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                                      <path d="M3 21h4l13 -13a1.5 1.5 0 0 0 -4 -4l-13 13v4" />
                                      <line x1="14.5" y1="5.5" x2="18.5" y2="9.5" />
                                      <polyline points="12 8 7 3 3 7 8 12" />
                                      <line x1="7" y1="8" x2="5.5" y2="9.5" />
                                      <polyline points="16 12 21 17 17 21 12 16" />
                                      <line x1="16" y1="17" x2="14.5" y2="18.5" />
                                    </svg>
                                 </button>`;
                break;
            case 'eliminar':
                grupoBotones += `<button type="button" class="navbar-brand btn btn-danger data-table-enlace" data-url="${enlace.href}" data-metodo="${enlace.metodo}" onclick="_administradorEventosGrupoEnlace(this)">
                                    <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-trash" width="32" height="32" viewBox="0 0 24 24" stroke-width="2.5" stroke="#ffffff" fill="none" stroke-linecap="round" stroke-linejoin="round">
                                      <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                                      <line x1="4" y1="7" x2="20" y2="7" />
                                      <line x1="10" y1="11" x2="10" y2="17" />
                                      <line x1="14" y1="11" x2="14" y2="17" />
                                      <path d="M5 7l1 12a2 2 0 0 0 2 2h8a2 2 0 0 0 2 -2l1 -12" />
                                      <path d="M9 7v-3a1 1 0 0 1 1 -1h4a1 1 0 0 1 1 1v3" />
                                    </svg>
                                 </button>`;
                break;
            case 'seleccionar':
                grupoBotones += `<button type="button" class="navbar-brand btn btn-info data-table-enlace" data-url="${enlace.href}" data-metodo="${enlace.metodo}" onclick="_administradorEventosGrupoEnlace(this)">
                                    <svg xmlns="http://www.w3.org/2000/svg" class="icon icon-tabler icon-tabler-list-search" width="32" height="32" viewBox="0 0 24 24" stroke-width="2.5" stroke="#ffffff" fill="none" stroke-linecap="round" stroke-linejoin="round">
                                      <path stroke="none" d="M0 0h24v24H0z" fill="none"/>
                                      <circle cx="15" cy="15" r="4" />
                                      <path d="M18.5 18.5l2.5 2.5" />
                                      <path d="M4 6h16" />
                                      <path d="M4 12h4" />
                                      <path d="M4 18h4" />
                                    </svg>
                                 </button>`;
            default:
                console.log('No es posible crear boton en datatable, la opción no existe.');
                break;
        }
    });
    grupoBotones += '</nav>';
    return grupoBotones;
};

const DataTableComplejo = () => {
    const dt = $('#dataTable').DataTable({
        processing: true,
        serverSide: true,
        order: [0, 'desc'],
        language: {
            url: "https://cdn.datatables.net/plug-ins/1.10.19/i18n/Spanish.json"
        },
        ajax: {
            headers: EndPointDataTableComplejo.headers,
            url: EndPointDataTableComplejo.url,
            type: EndPointDataTableComplejo.metodo
        },
        columns: [
            { data: "complejoId", title: "Clave", orderable: false, searchable: false },
            { data: "nombreSede", title: "Sede", orderable: false, searchable: false },
            { data: "nombreTipoComplejo", title: "Tipo de Complejo", orderable: false, searchable: false },
            { data: "nombreJefe", title: "Jefe", orderable: false, searchable: false },
            { data: "complejo", title: "Nombre", orderable: false, searchable: true },
            { data: "localizacion", title: "Localización", orderable: false, searchable: false },
            { data: "noArea", title: "No. Area", orderable: false, searchable: false },
            { data: "estado", title: "Estado", orderable: false, searchable: false },
            { data: "enlaces", title: " ", orderable: false, searchable: false, render: _construirGrupoEnlaces }
        ],
        destroy: true,
        lengthMenu: [5, 10, 25, 50]
    });
};

document.addEventListener('DOMContentLoaded', () => {
    DataTableComplejo();
    addEventInputsForm();
    cargarCatalogos();
});