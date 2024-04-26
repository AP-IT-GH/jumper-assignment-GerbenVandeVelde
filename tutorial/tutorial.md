# tutorial Jumper

- Tristan Dekeyser s140498
- Gerben Van de Velde

## inleideing

We gaan een simpele blok (agent) maken die over een moving target moet kunnen springen. Deze target zal elke episode sneller bewegen.

## setup
![img1](src/tekening.png) </br>
![img1](src/setup1.png) </br>
Hier hebben we onze basic setup met een target en een agent. </br>
![img1](src/setup2.png) </br>
We geven de agent alle components dat deze nodig heeft (Behaviour Parameters & Decision Requester). De agent en target hebben beide een rigid body en boxcollider. Ten slotte geven we de agent ook een ray perception sensor om te zien hoe dicht te target is zodat deze er over kan springen. </br>
![img1](src/setup3.png) </br>
Voor het laatste deel van de setup hebben we een empty object met een boxcollider. Als de target deze aanraakt dan betekent dit dus dat onze agent erover is gesprongen en krijgt hij een reward. </br>
## scripts
### variabelen
![img1](src/variabelen.png) </br>
### functies
![img1](src/functie1.png) </br>
Deze haalt de rigidbody’s op, neemt de startpositie van de agent en freezed de juiste assen zodat deze alleen kan springen. </br>
![img1](src/functie2.png) </br>
Deze ziet waar de target is en met welke snelheid deze op onze agent afkomt. </br>
![img1](src/functie3.png) </br>
Hier gaat hij beslissen of hij moet springen. </br>
![img1](src/functie4.png) </br>
Bij een nieuwe episode gaat hij de locaties resetten en een nieuwe snelheid aan de target geven </br>
![img1](src/functie5.png) </br>
Hiermee kan je hem handmatig laten springen met spatie voor testing. </br>
![img1](src/functie6.png) </br>
Dit is de methode waarmee hij springt. </br>
![img1](src/functie7.png) </br>
Hier wordt hij gestraft als hij de target aanraakt. </br>