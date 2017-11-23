var nba = require("nba");

var players = nba.players;

var bobTheJSONBuilder; 

//console.log(players);

nba.stats.playerInfo({ PlayerID: 201939 }).then(function(result){
  console.log("now");
  console.log(result);
}, function(err){
  console.log(err);
});
