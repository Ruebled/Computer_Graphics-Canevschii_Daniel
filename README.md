# Graphics_Homework

Canevschii Daniel - 3133A

- [x] Make a 3D cube
- [x] Make the cube translate on arrow key
- [x] Make camera move on middle mouse button click and drag
- [x] Change cube's face colors on 'c' Key to randoms ones
- [x] Make the cube resize on scroll
- [x] Make the cube rotate on x, z
- [ ] Toggle wireframe/fill mode on 'W' key
- [ ] Stop cube movement on plane borders
- [ ] Think more on plane rotation(unintuitive directions)



## Laborator 2 Questions
#### Ce este un viewport?
*Viewport* este o regiune dreptunghiulara unde imaginea finala randata e afisata.
#### Ce reprezintă conceptul de frames per seconds din punctul de vedere al bibliotecii OpenGL?
*Frames per seconds* sau *cadre pe secunda* sunt un parametru al scenei in opengl care descrie de cate ori
Imaginea(2D/3D), va fi rerandata intr-o secunda.
#### Când este rulată metoda OnUpdateFrame()?
De obicei aceasta este apelata inaintea functiei OnRenderFrame o data pe frame(30fps/60fps).
#### Ce este modul imediat de randare?
Modul imediat de randare presupune modul prin care primitivele precum puncte, linii si poligoane
sunt *desenate* pe ecran direct folosind functii.
Aceastea metoda e de o viteza mult mai mica spre deosebirea utilizarii *shadere-lor*, deoarece
trimit catre procesor putine date intrun moment, pe cand folosind shaderele datele obiectelor 
spre a fi randate se trimit in **batch** si nu incarca asa de tare CPU-ul.
#### Care este ultima versiune de OpenGL care acceptă modul imediat?
Pentru utilizarea modului imediat trebuie utilizata versiunea de OpenGL inainte de 3.0, 2.1 sau precedentele.
#### Când este rulată metoda OnRenderFrame()?
Aceasta este rulata odata pe frame, pentru 30fps aceasta reprezinta 1/30 secunda.
#### De ce este nevoie ca metoda OnResize() să fie executată cel puțin o dată?
Pentru a actualiza Obiectul ce trebuie randat in acelasi timp si viewport-ul pentru dimensiunea ecranului curent.

#### Ce reprezintă parametrii metodei CreatePerspectiveFieldOfView() și care este domeniul de valori pentru aceștia
- Field of view(FOV) - acesta specifica unghiul campului vertical vizibil in radiani sau grade(de obicei intre 0 si 180).
- Aspect Ratio - proportia dimensiunii ecranului randat pe orizontala catre dimensiune pe verticala;
- Near Clipping Plane - Parametru reprezinta cea mai apropiata distanta la care obiecte se randeaza.
- Far Clipping Plane - reprezinta distanta in departare la care obiectul se randeaza.

## References
-  