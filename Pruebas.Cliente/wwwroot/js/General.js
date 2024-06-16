function mostrarModalConfirmacion(mensaje, callbackAceptar) {
    $('#modalMensaje').text(mensaje);
    $('#modalConfirmacion').modal('show');

    // Manejar la acción de aceptar utilizando la función proporcionada
    $('#confirmBtn').on('click', function () {
        // Verificar si se proporcionó una función de callbackAceptar
        if (typeof callbackAceptar === 'function') {
            // Ejecutar la función de callbackAceptar
            callbackAceptar();
        }

        // Ocultar el modal después de ejecutar la acción
        $('#modalConfirmacion').modal('hide');
    });
}

function ocultarModalConfirmacion() {
    $('#modalConfirmacion').modal('hide');
}