CREATE   PROCEDURE SPRandomNumbers
@Count int
AS
EXEC SP_EXECUTE_EXTERNAL_SCRIPT 
     @language = N'Python', 
     @script = N'
import random

MyNumbers = []
for x in range(0,n):
	MyNumbers.append(random.randint(1,1001))
OutputDataSet = pandas.DataFrame(pandas.Series(MyNumbers))
', 
     @params = N'@n int
',
@n = @Count
WITH RESULT SETS (([RandomNumber] int))
