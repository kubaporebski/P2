Plik przedstawia og�lny opis procesu ETL.
Rozbito proces na dwie cz�ci. Cz�� pierwsza trwa do�� d�ugo i wykonywana mo�e by� raz na tydzie�. Cz�� druga trwa szybko i mo�e by� wykonywana cz�ciej.

Cz�� 1 (Part 1)
Pobranie najwa�nieszych element�w
	DFT_SUBJECTS_LEVEL_0
		Pobranie g��wnych temat�w.
	DFT_SUBJECTS_LEVEL_1
		Pobranie temat�w podrz�dnych.
	DFT_VARIABLES_LEVEL_0
		Pobranie g��wnych zmiennych.
	DFT_VARIABLES_LEVEL_1
		Pobranie podzmiennych.
	
Cz�� 2 (Part 2)
Pobieranie wymiar�w.
	DFT_ATTRIBUTES
		Pobranie atrybut�w
	DFT_MEASURES
		Pobranie miar
	DFT_UNITS
		Pobranie jednostek
	DFT_CHILDREN_VARIABLES
		Wymiary dla zmiennych

DATA_DOWNLOAD
DFT_DATABYVARIABLES
	Pobranie warto�ci dla zmiennych i ich wymiar�w do schematu `staging` uruchamiane jobem.

DATA_FROM_TMP_INTO_TABLE
DATA_FLOW_TASK
	Przeniesienie danych ze schematu `staging` do schematu `bdl`.
