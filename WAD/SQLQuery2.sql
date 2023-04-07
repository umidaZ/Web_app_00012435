use WadDB
go



INSERT INTO Habits ( Name, Frequency, Repeat, StartDate) 
VALUES ( '1 Habit', 3, 1, '2023-02-23')

INSERT INTO Progresses( [Name], [Date], [HabitProgress], [IsCompleted], [Notes], [HabitID]  ) 
VALUES ( 'My New Habit', '2023-02-23', 50, 0, 'AlmostDone', 2)
