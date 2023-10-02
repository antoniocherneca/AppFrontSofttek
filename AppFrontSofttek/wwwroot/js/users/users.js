var token = getCookie("Token");

let table = new DataTable('#users', {
  ajax: {
    url: `https://localhost:7106/api/Users`,
    dataSrc: "data.items",
    headers: { "Authorization": "Bearer " + token }
  },
  columns: [
    { data: 'userId', title: 'Id' },
    { data: 'userName', title: 'Nombre y Apellido' },
    { data: 'dni', title: 'DNI' },
    { data: 'email', title: 'E-mail' },
    { data: 'roleId', title: 'Id Rol' },
  ]
});

function addUser() {
  $.ajax({
    type: "GET",
    url: "/Users/UsersAddPartial",
    data: "",
    contentType: 'application/json',
    'dataType': "html",
    success: function (resultado) {
      $('#users-add-partial').html(resultado);
      $('#userModal').modal('show');
    }
  });
}