module QuadTreeBenchmarke

open FsCheck
open QuadTreeBenchmark
open QuadTreeBenchForMarking.QuadTreeGenerator
open FSharp.Charting

let timeit f v = 
    let watch = new System.Diagnostics.Stopwatch()
    watch.Start()
    let res = f v 
    watch.Stop()
    (watch.Elapsed.TotalMilliseconds, res)

let rec timeTreeNGrowing seed (fromN : int) (toN : int) incBy (genTree : int -> Gen<#QuadTree>) (fInTest : #QuadTree -> System.Collections.Generic.List<int * int>) : (int * float * int) seq =
    if fromN > toN then Seq.empty else
        seq {
            let tree = genTree fromN 
                        |> Gen.eval fromN seed

            let (runtime, res) = timeit fInTest tree

            yield (fromN, runtime, res.Count)
            yield! timeTreeNGrowing (snd <| Random.stdNext seed) (fromN + incBy) toN incBy genTree fInTest
        }

        
let seed = Random.mkStdGen 7L
let plotStart = 1000
let plotEnd = 3000
let step = 1000

let inside = Rectangle(Vector(-10000L, -10000L), Vector(10000L, 10000L))
let maxSize = Vector(5L, 300L)

let treeFact = LoserFactory()
let treeGenerator = genTree treeFact (genRectangle inside maxSize)  

let funcInTest (q : #QuadTree) = q.QueryOverlaps()

let queryTimePlots = 
    timeTreeNGrowing seed plotStart plotEnd step treeGenerator funcInTest
    |> List.ofSeq
    |> List.toSeq
    |> Seq.skip 1

let toTreeGraph (dataPoints : seq<(int * float)>) = 
    Chart.Line(dataPoints,"Avg QuadTree query time", (sprintf "Computation time of a query (Seed %A)" seed ))

let timePlots = queryTimePlots
                |> Seq.map (fun (n,t,_) -> (n,t))

let countPlots = queryTimePlots
                 |> Seq.map (fun (n,_,c) -> (n, float c))

toTreeGraph countPlots
|> Chart.Show

toTreeGraph timePlots
|> Chart.Show