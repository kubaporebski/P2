Opisy funkcji i og�lny plan dzia�ania SSIS.

	[dbo].[Attributes] -> staging.Attributes 
	Opisy specyficznych sytuacji w danych, kt�re powi�zane s� z ka�d� warto�ci� liczbow�, np. 'Warto�� mniejsza ni� przyj�ty format prezentacji'

	[dbo].[Measures] -> staging.Measures
	Jednostki miary wyst�puj�ce w danych, zwi�zane z konkretnymi zmiennymi, np. 'tysi�c litr�w'.

	[dbo].[Units] -> staging.TerritorialUnits
	Hierarchicznie powi�zana lista jednostek terytorialnych (od Polski do gmin w��cznie) i miejscowo�ci statystycznych.
	
	[dbo].[Subjects] -> staging.Subjects
	Hierarchicznie powi�zane grupy zmiennych wg zakresu merytorycznego, np. 'Ludno��', 'Ludno�� wg grup wieku i p�ci'.

	[dbo].[Variables] -> staging.Variables
	To wielowymiarowe cechy reprezentuj�ce okre�lone zjawisko, z okre�lonym obowi�zywaniem w latach i na konkretnym poziomie jednostek, 
	  np. liczba pracuj�cych dla wieku '20-26' i p�ci 'm�czy�ni'.
	W zale�no�ci od interesuj�cego nas tematu. Metoda przyjmuje parametr @parentId - id tematu. Wi�c nie musimy pobiera� wszystkich.
	
	[dbo].[DataByVariable] -> staging.Data
	Dane liczbowe w postaci tr�jki [liczba rzeczywista, identyfikator atrybutu, identyfikator roku], 
	  w postaci zbioru danych dla konkretnej zmiennej lub zbioru danych dla jednej jednostki.
	Metoda przyjmuje parametr @variableId - id zmiennej. Dodatkowo opcjonalny parametr @territorialUnitId - id jednostki terytorialnej (mo�na tu da� null - dla wszystkich)..


