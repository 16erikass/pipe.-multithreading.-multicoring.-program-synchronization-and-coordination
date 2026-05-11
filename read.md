Aprašymas

Programa yra sudaryta iš dviejų projektų - Master ir Agent.

Master programa sugeneruoja atsitiktinį skaičių, paleidžia du Agent procesus ir sukuria Named Pipe, prie kurio prisijungia abu agentai.

Kiekvienas agentas generuoja atsitiktinius skaičius ir bando atspėti Master sugeneruotą skaičių. Atspėjęs skaičių, agentas išsiunčia pranešimą per Named Pipe.

Master programa matuoja laiką nuo agentų paleidimo iki teisingo atsakymo gavimo ir nustato, kuris agentas pirmas atspėjo skaičių bei per kiek laiko tai padarė.