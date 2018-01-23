$(document).ready(function () {
    var $select = $('#school');
    
    $.getJSON('./data/jsonSchools.JSON', function(data){
            $select.html('');


            for(var i =0; i< data.length; i++){
              $select.append('<option id="'+ data[i]['Id'] + '">' +data[i]['School'] + '</option>');
            }

    });

    $("#FormDraft").submit(function(event) {
      
                      /* stop form from submitting normally */
                      event.preventDefault();
      
                      /* get some values from elements on the page: */
                      var $form = $(this),
                          term = $form.find('input[name="blocks"]').val(),
                          url = $form.attr('action');
                          console.log("ok")
                      /* Send the data using post */
                      var posting = $.post(url, {
                          s: term
                          
                      });
                        console.log($form )
                      /* Put the results in a div */
                      posting.done(function(data) {
                        console.log('ok')
                          var content = $(data).find('#content');
                         
                          console.log(content)
                      });
                  });

  });

  