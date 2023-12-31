/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP 1000 [DriverSinkID]
      ,[LocationID]
      ,[Index]
      ,[AddInsZone]
      ,[HMIComm]
  FROM [Len.Jakpro.JLRTSCADA].[dbo].[TblPAZoneList] where LocationID=5 order by [Index]

  update TblPAZoneList set DriverSinkID=209301001 where [Index]=5 and LocationID=7
  update TblPAZoneList set DriverSinkID=209301002 where [Index]=6 and LocationID=7
  update TblPAZoneList set DriverSinkID=209301003 where [Index]=7 and LocationID=7
  update TblPAZoneList set DriverSinkID=209301004 where [Index]=8 and LocationID=7

  update TblPAZoneList set DriverSinkID=209301101 where [index]=1 and LocationID=7
  update TblPAZoneList set DriverSinkID=209301102 where [index]=2 and LocationID=7
  update TblPAZoneList set DriverSinkID=209301103 where [index]=3 and LocationID=7
  update TblPAZoneList set DriverSinkID=209301104 where [index]=4 and LocationID=7

  update TblPAZoneList set DriverSinkID=209301105 where [index]=9 and LocationID=7
  update TblPAZoneList set DriverSinkID=209301106 where [index]=10 and LocationID=7

  update TblPAZoneList set DriverSinkID=209301107 where [index]=11 and LocationID=7
  update TblPAZoneList set DriverSinkID=209301108 where [index]=12 and LocationID=7


  update TblPAZoneList set DriverSinkID=201221001 where [Index]=4 and LocationID=6
  update TblPAZoneList set DriverSinkID=201221002 where [Index]=1 and LocationID=6
  update TblPAZoneList set DriverSinkID=201221003 where [Index]=2 and LocationID=6
  update TblPAZoneList set DriverSinkID=201221004 where [Index]=10 and LocationID=6
  update TblPAZoneList set DriverSinkID=201221005 where [Index]=11 and LocationID=6
  update TblPAZoneList set DriverSinkID=201221006 where [Index]=12 and LocationID=6
  update TblPAZoneList set DriverSinkID=201221007 where [Index]=3 and LocationID=6


  update TblPAZoneList set DriverSinkID=201221101 where [Index]=5 and LocationID=6
  update TblPAZoneList set DriverSinkID=201221102 where [Index]=6 and LocationID=6
  update TblPAZoneList set DriverSinkID=201221103 where [Index]=7 and LocationID=6
  update TblPAZoneList set DriverSinkID=201221104 where [Index]=8 and LocationID=6
  update TblPAZoneList set DriverSinkID=201221105 where [Index]=9 and LocationID=6

  update TblPAZoneList set DriverSinkID=201221107 where [Index]=13 and LocationID=6
  update TblPAZoneList set DriverSinkID=201221108 where [Index]=14 and LocationID=6


  update TblPAZoneList set DriverSinkID=201321001 where [Index]=1 and LocationID=5
  update TblPAZoneList set DriverSinkID=201321002 where [Index]=3 and LocationID=5
  update TblPAZoneList set DriverSinkID=201321003 where [Index]=2 and LocationID=5
  update TblPAZoneList set DriverSinkID=201321004 where [Index]=10 and LocationID=5
  update TblPAZoneList set DriverSinkID=201321005 where [Index]=11 and LocationID=5
  update TblPAZoneList set DriverSinkID=201321006 where [Index]=4 and LocationID=5  
  update TblPAZoneList set DriverSinkID=201321007 where [Index]=12 and LocationID=5

  update TblPAZoneList set DriverSinkID=201321101 where [Index]=6 and LocationID=5
  update TblPAZoneList set DriverSinkID=201321102 where [Index]=5 and LocationID=5
  update TblPAZoneList set DriverSinkID=201321103 where [Index]=7 and LocationID=5
  update TblPAZoneList set DriverSinkID=201321104 where [Index]=8 and LocationID=5
  update TblPAZoneList set DriverSinkID=201321105 where [Index]=9 and LocationID=5
  
  update TblPAZoneList set DriverSinkID=201321107 where [Index]=13 and LocationID=5
  update TblPAZoneList set DriverSinkID=201321108 where [Index]=14 and LocationID=5



  update TblPAZoneList set DriverSinkID=201421001 where [Index]=5 and LocationID=4
  update TblPAZoneList set DriverSinkID=201421002 where [Index]=2 and LocationID=4
  update TblPAZoneList set DriverSinkID=201421003 where [Index]=1 and LocationID=4
  update TblPAZoneList set DriverSinkID=201421004 where [Index]=11 and LocationID=4
  update TblPAZoneList set DriverSinkID=201421005 where [Index]=3 and LocationID=4
  update TblPAZoneList set DriverSinkID=201421006 where [Index]=4 and LocationID=4
  update TblPAZoneList set DriverSinkID=201421007 where [Index]=12 and LocationID=4

  update TblPAZoneList set DriverSinkID=201421101 where [Index]=6 and LocationID=4
  update TblPAZoneList set DriverSinkID=201421102 where [Index]=8 and LocationID=4
  update TblPAZoneList set DriverSinkID=201421103 where [Index]=7 and LocationID=4
  update TblPAZoneList set DriverSinkID=201421104 where [Index]=10 and LocationID=4
  update TblPAZoneList set DriverSinkID=201421105 where [Index]=9 and LocationID=4

  update TblPAZoneList set DriverSinkID=201421107 where [Index]=13 and LocationID=4
  update TblPAZoneList set DriverSinkID=201421108 where [Index]=14 and LocationID=4

  
  update TblPAZoneList set DriverSinkID=201521001 where [Index]=5 and LocationID=3
  update TblPAZoneList set DriverSinkID=201521002 where [Index]=3 and LocationID=3
  update TblPAZoneList set DriverSinkID=201521003 where [Index]=2 and LocationID=3
  update TblPAZoneList set DriverSinkID=201521004 where [Index]=4 and LocationID=3
  update TblPAZoneList set DriverSinkID=201521005 where [Index]=11 and LocationID=3
  update TblPAZoneList set DriverSinkID=201521006 where [Index]=1 and LocationID=3
  update TblPAZoneList set DriverSinkID=201521007 where [Index]=12 and LocationID=3

  update TblPAZoneList set DriverSinkID=201521101 where [Index]=7 and LocationID=3
  update TblPAZoneList set DriverSinkID=201521102 where [Index]=10 and LocationID=3
  update TblPAZoneList set DriverSinkID=201521103 where [Index]=8 and LocationID=3
  update TblPAZoneList set DriverSinkID=201521104 where [Index]=6 and LocationID=3
  update TblPAZoneList set DriverSinkID=201521105 where [Index]=9 and LocationID=3

  update TblPAZoneList set DriverSinkID=201521107 where [Index]=13 and LocationID=3
  update TblPAZoneList set DriverSinkID=201521108 where [Index]=14 and LocationID=3


  update TblPAZoneList set DriverSinkID=201621001 where [Index]=5 and LocationID=2
  update TblPAZoneList set DriverSinkID=201621002 where [Index]=7 and LocationID=2
  update TblPAZoneList set DriverSinkID=201621003 where [Index]=2 and LocationID=2
  update TblPAZoneList set DriverSinkID=201621004 where [Index]=11 and LocationID=2
  update TblPAZoneList set DriverSinkID=201621005 where [Index]=12 and LocationID=2
  update TblPAZoneList set DriverSinkID=201621006 where [Index]=9 and LocationID=2
  update TblPAZoneList set DriverSinkID=201621007 where [Index]=4 and LocationID=2

  update TblPAZoneList set DriverSinkID=201621101 where [Index]=6 and LocationID=2
  update TblPAZoneList set DriverSinkID=201621102 where [Index]=1 and LocationID=2
  update TblPAZoneList set DriverSinkID=201621103 where [Index]=10 and LocationID=2
  update TblPAZoneList set DriverSinkID=201621104 where [Index]=8 and LocationID=2
  update TblPAZoneList set DriverSinkID=201621105 where [Index]=3 and LocationID=2

  update TblPAZoneList set DriverSinkID=201621107 where [Index]=13 and LocationID=2
  update TblPAZoneList set DriverSinkID=201621108 where [Index]=14 and LocationID=2


