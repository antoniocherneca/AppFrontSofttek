function setCookie(name, value, days) {
  var expiration = "";
  if (days) {
    var fecha = new Date();
    fecha.setTime(fecha.getTime() + (days * 24 * 60 * 60 * 1000));
    expiration = "; expires=" + fecha.toUTCString();
  }

  document.cookie = name + "=" + (value || "") + expiration + "; path=/"
}

function getCookie(name) {
  var name = name + "=";
  var cookies = document.cookie.split(";");

  for (var i = 0; i < cookies.length; i++) {
    var c = cookies[i];
    while (c.charAt(0) == ' ') c = c.substring(1, c.length);
    if (c.indexOf(name) == 0) return c.substring(name.length, c.length);
  }
}