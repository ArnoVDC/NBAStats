$(document).ready(function () {
  var $select = $('#school');

  $.getJSON('./data/jsonSchools.JSON', function (data) {
    $select.html('');
    console.log(data[123]);

    for (var i = 0; i < data.length; i++) {
      $select.append('<option id="' + data[i]['Id'] + '" value="' + data[i]['Id'] + '" >' + data[i]['School'] + '</option>');
    }

  });
});

function disableSection(id){
  document.getElementById(id).className="invissible";
}
function enableSection(id){
  document.getElementById(id).className="visible";
}

function gotPicked(){
  disableSection("entry");
  enableSection("picked");
}
function notPicked(){
  disableSection("entry");
  enableSection("notPicked");
}
function newEntry(){
  enableSection(id);
}


function post(form) {
  var http = new XMLHttpRequest();
  var url = "";
  var school = document.getElementById("school").options;
  var params = "school=" + document.getElementById("school").value +
    ";G=" + document.getElementById("G").value +
    ";FG=" + document.getElementById("FG").value +
    ";P2=" + document.getElementById("2P").value +
    ";P3=" + document.getElementById("3P").value +
    ";FT=" + document.getElementById("FT").value +
    ";TRB=" + document.getElementById("TRB").value +
    ";AST=" + document.getElementById("AST").value +
    ";STL=" + document.getElementById("STL").value +
    ";BLK=" + document.getElementById("BLK").value +
    ";PTS=" + document.getElementById("PTS").value + ";"


  http.open("POST", url, true);
  http.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
  http.onreadystatechange = function () {//Call a function when the state changes.
    if (http.readyState == 4 && http.status == 200) {
      if(http.responseText == "true"){
        //picked
        gotPicked();
      }
      else{
        //notpicked
        notPicked();
      }
    }
  }

  http.send(params);
  console.log(params);
  console.info("post send");
}
console.log("script loaded");
