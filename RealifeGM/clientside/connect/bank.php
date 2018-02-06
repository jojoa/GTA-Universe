<html>
<head>
<style>
        .main {
            width: 360px;
            padding: 8% 0 0;
            margin: auto;
        }

        .img {
            box-shadow: 0 0 20px 0 rgba(0,0,0,0.2), 0 5px 5px 0 rgba(0,0,0,0.24);
        }

        .form {
            position: relative;
            z-index: 1;
            background: #FFFFFF;
            max-width: 360px;
            margin: 0 auto 100px;
            padding: 45px;
            text-align: center;
            box-shadow: 0 0 20px 0 rgba(0,0,0,0.2), 0 5px 5px 0 rgba(0,0,0,0.24);
        }

            .form input {
                font-family: "Roboto", sans-serif;
                outline: 0;
                background: #f2f2f2;
                width: 100%;
                border: 0;
                margin: 0 0 15px;
                padding: 15px;
                box-sizing: border-box;
                font-size: 14px;
            }

            .form button {
                font-family: "Roboto", sans-serif;
                text-transform: uppercase;
                outline: 0;
                background: #4CAF50;
                width: 100%;
                border: 0;
                padding: 15px;
                color: #FFFFFF;
                font-size: 14px;
                -webkit-transition: all 0.3 ease;
                transition: all 0.3 ease;
                cursor: pointer;
            }

                .form button:hover, .form button:active, .form button:focus {
                    background: #43A047
                }

            

            form { display:none; }

.closed { background:red; }

.open { background:green; }
			
			.initial-open {display:block;}

        .container {
            position: relative;
            z-index: 1;
            max-width: 300px;
            margin: 0 auto;
        }

            .container:before .container:after {
                content: "";
                display: block;
                clear: both;
            }

            .container .info {
                margin: 50px auto;
                text-align: center;
            }

                .container .info h1 {
                    margin: 0 0 15px;
                    padding: 0;
                    font-size: 36px;
                    font-weight: 300;
                    color: #1a1a1a;
                }

                .container .info span {
                    color: #4d4d4d;
                    font-size: 12px;
                }

                    .container .info span a {
                        color: #000000;
                        text-decoration: none;
                    }

                    .container .info span .fa {
                        color: #EF3B3A;
                    }

        body {
            background-color: transparent;
            font-family: "Roboto", sans-serif;
            -webkit-font-smoothing: antialiased;
            -moz-osx-font-smoothing: grayscale;
        }
</style>
</head>
<body>
<div class="main">
<div class="form">
<p id="knt">Kontonummer:</p>
<br>
<button class="closed" id="Einzahlen">Einzahlen</button>
<form class="menu">
<input type="number" id="moneyein" placeholder="Geldbetrag" />
<button id="einzahlbtn">Einzahlen</button>
</form>
<button class="closed" id="auszahlen">Auszahlen</button>
<form class="menu">
<input type="number" id="moneyaus" placeholder="Geldbetrag" />
<button id="auszahlbtn">Auszahlen</button>
</form>
<button class="closed" id="uberweisen">Überweisen</button>
<form class="menu">
<input type="number" id="moneyuber" placeholder="Geldbetrag" />
<input type="number" id="empfuber" placeholder="Empfänger" />
<button id="uberweisbtn">Überweisen</button>
</form>






</div>
</div>
<script src='js/jquery.js'></script>
</body>
</html>

<script>

  $(document).ready(function(){
 $("button").click(function(){ 
  $('form').slideUp("fast");
  $('button').removeClass('open').addClass("closed");
  $(this).next("form").slideDown("fast"); 
 });
});


  
  
  
document.getElementById("einzahlbtn").onclick = function () {



	        if (moneyein && moneyein.value) {
                resourceCall("einzahl", moneyein.value);
                
			}
  }
  
document.getElementById("auszahlbtn").onclick = function () {

	        if (moneyaus && moneyaus.value) {
                resourceCall("auszahl", moneyaus.value);
                
			}
  }
  
document.getElementById("uberweisbtn").onclick = function () {

	        if (moneyuber && moneyuber.value) {
                resourceCall("uberweis", moneyuber.value, empfuber.value);
                
			}
  }
  
 


</script> 