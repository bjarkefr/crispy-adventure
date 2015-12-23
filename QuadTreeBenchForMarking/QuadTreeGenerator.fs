namespace QuadTreeBenchForMarking

module QuadTreeGenerator =
    open QuadTreeBenchmark
    open FsCheck

//    let genRange (inside : Range) =
//        gen {            
//            let lowResMin = int <| inside.Min
//            let lowResMax = int <| inside.Max
//            let! max = Gen.choose(lowResMin + 1 , lowResMax)
//            let! min = Gen.choose(lowResMin    , max)
//            return Range.range (int64 min) (int64 max)
//        }
//
//    let genSquare (inside : Rectangle) =
//        gen {
//            let! rangeX = genRange inside.X
//            let! rangeY = genRange inside.Y
//            return { X = rangeX; Y = rangeY }
//        }
//
//    let genRectangle (l : int) (inside : Rectangle) =
//        gen {
//            let! w = int64 <!> Gen.choose(1, l)
//            let! h = int64 <!> Gen.choose(1, l)
//            let! posX = int64 <!> Gen.choose (int inside.X.Min, int inside.X.Max)
//            let! posY = int64 <!> Gen.choose (int inside.Y.Min, int inside.Y.Max)
//            return Rectangle.fromPos (posX, posY) w h
//        } 
//
//    let toThings squares = 
//        let makeThing id sq = { Id = int64 id; Bounding = sq }
//        List.mapi makeThing squares
//
//    let genThings square =
//        gen {
//            let! squares = Gen.listOf square
//            return toThings squares
//        }

    let inline genLong min max = int64 <!> (Gen.choose (int min, int max))

    let genRectangle (inside: Rectangle) (maxSize: Vector) =
        gen {
            let! w = genLong maxSize.X maxSize.Y
            let! h = genLong maxSize.X maxSize.Y

            let! x = genLong inside.Bl.X (inside.Tr.X - w)
            let! y = genLong inside.Bl.Y (inside.Tr.Y - h)

            return Rectangle (Vector (x, y), Vector(x + w, y + h))
        }

    let genTree (factory : QuadTreeFactory<'a>) (rectGen : Gen<Rectangle>) n =
        gen {
           let! rects = Gen.listOfLength n rectGen
           let tree = factory.CreateTree()
           List.fold (fun () r -> ignore <| tree.Add(r)) () rects
           return tree
        }
