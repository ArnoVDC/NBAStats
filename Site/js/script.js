$(document).ready(function () {
    var $select = $('#school');
    
    $.getJSON('./data/jsonSchools.JSON', function(data){
            $select.html('');
            console.log(data[123]);

            for(var i =0; i< data.length; i++){
              $select.append('<option id="'+ data[i]['Id'] + '">' +data[i]['School'] + '</option>');
            }

    });
  });

