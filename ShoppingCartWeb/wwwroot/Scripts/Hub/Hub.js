

    $(function (){
        var mapHub = $.connection.mapHub;

        registerClientMethods(mapHub);

        $.connection.hub.start().done(function () {
            registerEvents(mapHub)
        });

    });

function registerEvents(mapHub) {
    chatHub.server.connect('123');
}

function registerClientMethods(mapHub) {
    mapHub.client.onConnected = function (id, numberProducts) {
        console.log("hola");
    }
}

function UpdateShppingCartNumber(mapHub, number) {

    var myLatLng = new google.maps.LatLng(Latitud, Longitud);
    var contentString = '<div class="map-data">' + '<h6>' + Evento + '</h6>' + '<div class="map-content">' + Direccion + '<br><br><a href="#" style="text-decoration: none;cursor: pointer;color: #a1c436;">' + UserName + '</a></div>' + '</div>';

    var infowindow = new google.maps.InfoWindow({
        content: contentString
    });



    var marker = new google.maps.Marker({
        position: myLatLng,
        map: map,
    });

    map.setCenter(marker.getPosition());

    google.maps.event.addListener(marker, 'click', function () {
        infowindow.open(map, marker);
    });
}




function registerClientMethods(mapHub) {

    //// Se llama a esta funcion cuando un usuario inicia sesion
    mapHub.client.onConnected = function (id, userName, allUsers, messages) {

        //$('#hdId').val(id);
        //$('#hdUserName').val(userName);
        //$('#spanUser').html(userName);

        // Añadimos todos los usuarios a nuestro sidebar de la vista
        for (i = 0; i < allUsers.length; i++) {
            AddUser(mapHub, allUsers[i].ConnectionId, allUsers[i].Nombres, allUsers[i].Latitud, allUsers[i].Longitud);
        }

        //// Add Existing Messages
        //for (i = 0; i < messages.length; i++) {
        //    AddMessage(messages[i].UsuarioId, messages[i].UserName, messages[i].Message, messages[i].Foto);
        //}


    }

    // On New User Connected
    mapHub.client.onNewUserConnected = function (UserName, Direccion, Latitud, Longitud, Evento) {
        AddUser(mapHub, UserName, Direccion, Latitud, Longitud, Evento);
    }

    // On User Disconnected
    mapHub.client.onUserDisconnected = function (id, userName) {

    }

    mapHub.client.messageReceived = function (userId, userName, message, foto) {
        AddMessage(userId, userName, message, foto);
    }

    mapHub.client.sendPrivateMessage = function (windowId, fromUserName, message) {
    }

}




