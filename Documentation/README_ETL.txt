Plik przedstawia ogólny opis procesu ETL.
Rozbito proces na dwie czêœci. Czêœæ pierwsza trwa doœæ d³ugo i wykonywana mo¿e byæ raz na tydzieñ. Czêœæ druga trwa szybko i mo¿e byæ wykonywana czêœciej.

Czêœæ 1 (Part 1)
Pobranie najwa¿nieszych elementów
	DFT_SUBJECTS_LEVEL_0
		Pobranie g³ównych tematów.
	DFT_SUBJECTS_LEVEL_1
		Pobranie tematów podrzêdnych.
	DFT_VARIABLES_LEVEL_0
		Pobranie g³ównych zmiennych.
	DFT_VARIABLES_LEVEL_1
		Pobranie podzmiennych.
	
Czêœæ 2 (Part 2)
Pobieranie wymiarów.
	DFT_ATTRIBUTES
		Pobranie atrybutów
	DFT_MEASURES
		Pobranie miar
	DFT_UNITS
		Pobranie jednostek
	DFT_CHILDREN_VARIABLES
		Wymiary dla zmiennych

DATA_DOWNLOAD
DFT_DATABYVARIABLES
	Pobranie wartoœci dla zmiennych i ich wymiarów do schematu `staging` uruchamiane jobem.

DATA_FROM_TMP_INTO_TABLE
DATA_FLOW_TASK
	Przeniesienie danych ze schematu `staging` do schematu `bdl`.
