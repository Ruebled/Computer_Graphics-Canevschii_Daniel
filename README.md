# Graphics_Homework

Canevschii Daniel - 3133A

- [x] Make a 3D cube
- [x] Make the cube translate on arrow key
- [x] Make camera move on middle mouse button click and drag
- [x] Change cube's face colors on 'c' Key to randoms ones
- [x] Make the cube resize on scroll
- [x] Make the cube rotate on x, z
- [x] Toggle wireframe/fill mode on 'W' key
- [ ] Stop cube movement on plane borders
- [ ] Think more on plane rotation(unintuitive directions)



## Lab 2 Questions
### Ce este un viewport?
*Viewport* este o regiune dreptunghiulara unde imaginea finala randata e afisata.

### Ce reprezintă conceptul de frames per seconds din punctul de vedere al bibliotecii OpenGL?
*Frames per seconds* sau *cadre pe secunda* sunt un parametru al scenei in opengl care descrie de cate ori
Imaginea(2D/3D), va fi rerandata intr-o secunda.

### Când este rulată metoda OnUpdateFrame()?
De obicei aceasta este apelata inaintea functiei OnRenderFrame o data pe frame(30fps/60fps).

### Ce este modul imediat de randare?
Modul imediat de randare presupune modul prin care primitivele precum puncte, linii si poligoane
sunt *desenate* pe ecran direct folosind functii.
Aceastea metoda e de o viteza mult mai mica spre deosebirea utilizarii *shadere-lor*, deoarece
trimit catre procesor putine date intrun moment, pe cand folosind shaderele datele obiectelor 
spre a fi randate se trimit in **batch** si nu incarca asa de tare CPU-ul.

### Care este ultima versiune de OpenGL care acceptă modul imediat?
Pentru utilizarea modului imediat trebuie utilizata versiunea de OpenGL inainte de 3.0, 2.1 sau precedentele.

### Când este rulată metoda OnRenderFrame()?
Aceasta este rulata odata pe frame, pentru 30fps aceasta reprezinta 1/30 secunda.

### De ce este nevoie ca metoda OnResize() să fie executată cel puțin o dată?
Pentru a actualiza Obiectul ce trebuie randat in acelasi timp si viewport-ul pentru dimensiunea ecranului curent.

### Ce reprezintă parametrii metodei CreatePerspectiveFieldOfView() și care este domeniul de valori pentru aceștia
- Field of view(FOV) - acesta specifica unghiul campului vertical vizibil in radiani sau grade(de obicei intre 0 si 180).
- Aspect Ratio - proportia dimensiunii ecranului randat pe orizontala catre dimensiune pe verticala;
- Near Clipping Plane - Parametru reprezinta cea mai apropiata distanta la care obiecte se randeaza.
- Far Clipping Plane - reprezinta distanta in departare la care obiectul se randeaza.

---

## Lab 3 Questions
### 1. Care este ordinea de desenare a vertexurilor pentru aceste metode(orar sau anti-orar)? 
#### Desenați axele de coordonate din aplicația-template folosind un singur apel GL.Begin().

### 2. Ce este anti-aliasing? Prezentați această tehnică pe scurt. 
- Procedeul de anti-aliasing(*multisampling*) se caracterizeaza prin definirea a mai multor *subpixeli* intr-un 
pixel ce necesita randat. Daca se doreste randarea unui tringhiu ce are latura pozitionata diagonal retelei 
de pixeli, si neutilizand "multisampling-ul" obtinem o linie "scarita". 
Procedeul de multisampling presupune observarii procentajului din pixel acoperit de linie, conform caruia se 
calculeaza culoarea finala a pixelului, ce determina o randare mai "smooth". 
- Numarul de *subpixeli* folositi pentru *multisampling* se declara drept al patrulea argument in obiectul **GraphicsMode**.

### 3. Care este efectul rulării comenzii GL.LineWidth(float)? 
Seteaza grosimea liniilor ce urmeaza a fi randate.
#### Dar pentru GL.PointSize(float)? 
Seteaza dimensiunea punctelor ce urmeaza a fi randate.(state machine principle)
#### Funcționează în interiorul unei zone GL.Begin()?
Apelarea metodelor precum GL.PointSize(float) sau GL.LineWidth(float) nu se "proceseaza" daca apelul lor se petrece in 
blocurile GL.Begin(), GL.End();

### 4. Răspundeți la următoarele întrebări (utilizați ca referință eventual și tutorii OpenGL Nate Robbins):
#### Care este efectul utilizării directivei LineLoop atunci când desenate segmente de dreaptă multiple în OpenGL?
#### Care este efectul utilizării directivei LineStrip atunci când desenate segmente de dreaptă multiple în OpenGL?
#### Care este efectul utilizării directivei TriangleFan atunci când desenate segmente de dreaptă multiple în OpenGL?
#### Care este efectul utilizării directivei TriangleStrip atunci când desenate segmente de dreaptă multiple în OpenGL?

### 5. Creați un proiect elementar. Urmăriți exemplul furnizat cu titlu de demonstrație - OpenGL_conn_ImmediateMode. 
Atenție la setarea viewport-ului.

### 6. Urmăriți aplicația „shapes.exe” din tutorii OpenGL Nate Robbins.
#### De ce este importantă utilizarea de culori diferite (în gradient sau culori selectate per suprafață) în desenarea obiectelor 3D? 
Daca nu se utilizeaza umbre, pentru a observa caracteristica 3D a obiectului se utilizeaza diferite culori pe suprafata, ce 
ofera "volum" formei.

### 7. Ce reprezintă un gradient de culoare? Cum se obține acesta în OpenGL?
Un gradient presupune un patern de coloare care reprezinta o trecere "smooth" de la o culoare  la alta(sau mai multe),
precum curcubeul...
In OpenTK aceasta se face atribuind fiecarui *Vertex* din figura (Line, Triangle, Quads) ce necesita randata, si 
activatea obtiunii *GL.Enable(EnableCap.DepthTest);* pentru obtinerea gradientului pe figura.

### 8. Creați o aplicație care la apăsarea unui set de taste va modifica culoarea unui triunghi 
### între valorile minime și maxime, pentru fiecare canal de culoare. 
#### Ce efect va apare la utilizarea canalului de transparență?
#### Aplicația va permite modificarea unghiului camerei cu ajutorul mouse-ului. 

### 9. Modificați aplicația pentru a manipula valorile RGB pentru fiecare vertex ce definește un triunghi. Afișați valorile RGB în consolă.

### 10. Ce efect are utilizarea unei culori diferite pentru fiecare vertex atunci când desenați o linie sau un triunghi în modul strip?

## References
-  