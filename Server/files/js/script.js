$(document).ready(function () {
  var $select = $('#school');

  $.getJSON('./data/jsonSchools.JSON', function (data) {
    $select.html('');
    console.log(data[123]);

    for (var i = 0; i < data.length; i++) {
      $select.append('<option id="' + data[i]['Id'] + '">' + data[i]['School'] + '</option>');
    }

  });
});

function post(form) {
  var http = new XMLHttpRequest();
  var url = "";
  var params = "school=hello,G=hello,MP=hello,PTS=hello,TRB=hello,AST=hello,STL=hello,BLK=hello,FG%=hello,2P%=hello,3P%=hello,FT%=hello,WS=hello,WS=hello";
  http.open("POST", url, true);
  http.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
  http.onreadystatechange = function () {//Call a function when the state changes.
    if (http.readyState == 4 && http.status == 200) {
      alert(http.responseText);
      
    }
  }
  http.send(params);
  console.log(params);
  console.info("post send");
  return false; //so the page doesn't reload
}
console.log("script loaded");
