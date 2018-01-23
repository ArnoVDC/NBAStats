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
  var params = school=document.getElementById("school").value;G=document.getElementById("G").value;MP=document.getElementById("MP").value;
  PTS=document.getElementById("PTS").value;TRB=document.getElementById("TRB").value,AST=document.getElementById("AST").value,STL=document.getElementById("STL").value;
  BLK=document.getElementById("BLK").value;
  FG=document.getElementById("FG").value;P2=document.getElementById("2P").value;P3=document.getElementById("3P").value;
  FT=document.getElementById("FT").value;
  WS=document.getElementById("WS").value;WS=document.getElementById("WS1").value;
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
}
console.log("script loaded");
