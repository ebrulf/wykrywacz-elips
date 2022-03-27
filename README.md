# wykrywacz-elips
pozaprogramowy projekt na przedmiot "Wstęp do Programowania"

## Historia
Program powstał w styczniu 2021 jako ponadobowiązkowe wyzwanie na studiach informatycznych.
### Treść zadania
Za: [https://e.sggw.pl/mod/assign/view.php?id=136813 Wyzwanie #3 - Granice zaznaczeń]
>1) Napisz program, który dla prostokątów zaznaczonych na obrazie w programie Paint na czerwono zwróci plik zawierający ich współrzędne w kolejnych liniach w formacie "nr xmin xmax ymin ymax".
>Przykładowo, plik IMG_9080_zaznaczenia.txt jest oczekiwanym wynikiem działania programu dla obrazu IMG_9080_zaznaczenia.png
>2) Napisz program, który będzie na obrazie rysował zielone prostokąty na podstawie pliku tekstowego, o formacie jak z programu pierwszego
>3) Napisz program, który rozwiąże zadanie pierwsze, również w przypadku nakładania zaznaczeń (patrz plik zaznaczenia_3_prostokaty.png)
>4) Napisz program, który będzie działał dla oznaczeń w kształcie elips (patrz plik zaznaczenia_elipsy.png)
>
## Działanie
Program w kolejności:
* sprawdza, czy działa biblioteka System.Drawing.Common
* wykrywa prostokąty czerwone nienachodzące na siebie
* wykrywa prostokąty czarne nachodzące na siebie
* rysuje prostokąty zielone na podstawie listy z punktu drugiego
* wykrywa elipsy czarne nachodzące na siebie
* rysuje elipsy czarne na podstawie listy z punktu czwartego

Program działa w 100% dla prostokątów i nienachodzących na siebie elips. Dla przecinających się elips program działa w 80%.

Program działa na platofrmie .NET 3.1.0.
## Licencja
Projekt udostępniony do wglądu, jak na razie wszelkie prawa zastrzeżone.

Przerobione zdjęcie Juliana Tuwima - przeróbka autorska.
