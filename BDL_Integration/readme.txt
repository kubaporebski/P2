Opisy funkcji i ogólny plan dzia³ania SSIS.

	[dbo].[Attributes] -> staging.Attributes 
	Opisy specyficznych sytuacji w danych, które powi¹zane s¹ z ka¿d¹ wartoœci¹ liczbow¹, np. 'Wartoœæ mniejsza ni¿ przyjêty format prezentacji'

	[dbo].[Measures] -> staging.Measures
	Jednostki miary wystêpuj¹ce w danych, zwi¹zane z konkretnymi zmiennymi, np. 'tysi¹c litrów'.

	[dbo].[Units] -> staging.TerritorialUnits
	Hierarchicznie powi¹zana lista jednostek terytorialnych (od Polski do gmin w³¹cznie) i miejscowoœci statystycznych.
	
	[dbo].[Subjects] -> staging.Subjects
	Hierarchicznie powi¹zane grupy zmiennych wg zakresu merytorycznego, np. 'Ludnoœæ', 'Ludnoœæ wg grup wieku i p³ci'.

	[dbo].[Variables] -> staging.Variables
	To wielowymiarowe cechy reprezentuj¹ce okreœlone zjawisko, z okreœlonym obowi¹zywaniem w latach i na konkretnym poziomie jednostek, 
	  np. liczba pracuj¹cych dla wieku '20-26' i p³ci 'mê¿czyŸni'.
	W zale¿noœci od interesuj¹cego nas tematu. Metoda przyjmuje parametr @parentId - id tematu. Wiêc nie musimy pobieraæ wszystkich.
	
	[dbo].[DataByVariable] -> staging.Data
	Dane liczbowe w postaci trójki [liczba rzeczywista, identyfikator atrybutu, identyfikator roku], 
	  w postaci zbioru danych dla konkretnej zmiennej lub zbioru danych dla jednej jednostki.
	Metoda przyjmuje parametr @variableId - id zmiennej. Dodatkowo opcjonalny parametr @territorialUnitId - id jednostki terytorialnej (mo¿na tu daæ null - dla wszystkich)..


