var nba = require("nba");
var fs = require('fs');

var players = nba.players;

var bobTheJSONBuilder; 

//console.log(players);

nba.stats.playerInfo({PlayerID: 201939}).then(function(result) {
  console.log(result)

  for(var i = 0; i < result.availableSeasons.length; i++){
    //console.log(result.availableSeasons[i]);

    nba.stats.playerSplits({ PlayerID: 201939 }).then(function(stats){
      console.log("before stats save");
      fs.writeFile("./tmp/test.json", stats.tostring(), function(err) {
        if(err) {
            return console.log(err);
        }
        console.log("The file was saved!");

    }); 
    }, function(err){
      console.log(err);
    });
    
  }

})


