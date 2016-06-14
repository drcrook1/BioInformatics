// Learn more about F# at http://fsharp.org. See the 'F# Tutorial' project
// for more guidance on F# programming.

#load "./Utilities/CSVWriter.fs"
open BioInfo.Web.Analytics.Services.Utilities
open System.IO


    //Example
type Test(colA:string, colB:int) = 
    member x.ColA = colA
    member x.ColB = colB

let testData = seq { for i in 1..10 -> new Test("col"+string(i), i) }

// using all public class properties for serialization
testData
|> Seq.csv "\t" (fun propertyName -> propertyName)
|> Seq.write "test_with_class_properties.csv"

// using a tuple projection
testData
|> Seq.distinctBy (fun testInstance -> testInstance.ColA)  
|> Seq.map (fun probe -> (probe.ColB) )
|> Seq.csv "\t" (fun columnName -> 
                    match columnName with 
                    | "0" -> "ColB"
                    | _ -> columnName)

System.DateTime.UtcNow.ToString()