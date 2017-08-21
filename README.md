# SmallPrograms
<h4>
    Aplikacja internetowa, na którą składają się cztery niewielkie programy:
</h4>
<ul>
    <li>
        Perceptron
        <div class="alert alert-info">
            Perceptron dyskretny, bipolarny. Aktywuje się, gdy suma współrzędnych X i Y jest większa od podanego przez użytkownika progu.<br />
            Współrzędne w zbiorze treningowym są z przedziału [-100, 100].
            <br />
            <a href="http://users.pja.edu.pl/~msyd/wyk-nai/perceptron3-pl.pdf">Informacje nt. perceptronu</a>
        </div>
    </li>
    <li>
        K-means
        <div class="alert alert-info">
            <a href="https://pl.wikipedia.org/wiki/Algorytm_centroid%C3%B3w">https://pl.wikipedia.org/wiki/Algorytm_centroid%C3%B3w</a>
        </div>
    </li>
    <li>
        Szyfr monoalfabetyczny
        <div class="alert alert-info">
            Tekst szyfrowany jest w następujący sposób:<br>
            liczone jest ile razy dana litera (od a do z, bez polskich liter) występuje w tekście wzorcowym,
            a następnie litery sortowane są malejąco ze względu na ilośc wystąpień. Jeśli kilka liter występuje tyle samo razy,
            wyżej jest ta, która jest wcześniej w alfabecie. W ten sposób otrzymujemy tajny alfabet
            i na jego podstawie tworzony jest szyfr. Wielkość liter nie ma znaczenia, zarówno w tekście jawnym
            jak i przy zliczniu wystąpień liter w tekście wzorcowym. Litery nie wchodzące w zkład alfabetu
            oraz znaki nie są szyfrowane.
        </div>
    </li>
    <li>
        Szyfr DES
        <div class="alert alert-info">
            Szyfruje/deszyfruje tekst zapisany w postaci dwójkowej lub szesnastkowej, przy pomocy podanego klucza.
            Tekst nie może zawierać znaku enter (może natomiast zawierać spacje, podobnie jak klucz).
        </div>
    </li>
</ul>
