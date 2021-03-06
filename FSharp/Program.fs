open System
open Models
open Utils
open Operators

let problem1 () =
    let limit = 1000
    let multiplesTargets = [ 3; 5 ]

    multiples limit multiplesTargets
    |> Seq.distinct
    |> Seq.sum

let problem2 () =
    ()
    |> fib
    |> Seq.takeWhile (fun i -> i < bigint 4000000)
    |> Seq.filter (fun i -> i % bigint 2 = bigint.Zero)
    |> Seq.sum

let problem3 () =
    let target = 600851475143L

    target |> factors |> Seq.filter isPrime |> Seq.max

let problem4 () =
    let generateProducts limit bottom =
        let range = [ bottom .. limit ]

        seq {
            for i in range do
                for j in range do
                    yield i * j
        }

    100
    |> generateProducts 999
    |> Seq.map bigint
    |> Seq.filter isPalindrome
    |> Seq.max

let problem5 () =
    let checkRange range n =
        if n % 5000000 = 0 then printfn "Testing: %d" n
        range |> Seq.forall (n |> int64 |> isDivisible)

    let range =
        [ 1 .. 20 ]
        |> List.map (fun x -> x |> int64)
        |> Seq.ofList

    // Generate an infinite list starting at 1
    Seq.initInfinite (fun i -> i + 1)
    // Create a list of every number that is not divisible by every number in the range
    |> Seq.takeWhile (fun i -> checkRange range i |> not)
    |> List.ofSeq
    // The answer is the last element + 1
    |> List.last
    |> fun i -> i + 1


let problem6 () =
    let sumOfSquares range =
        List.fold (fun acc current -> acc + Math.Pow(current, 2.0)) 0.0 range
        |> int

    let squareOfSum range =
        range
        |> List.sum
        |> fun x -> Math.Pow(x, 2.0) |> int


    let range = [ 1.0 .. 100.0 ]
    squareOfSum range - sumOfSquares range

let problem7 () =
    generateNPrimes 100000000 |> Seq.item 10000


// Not complete
let problem8 () =
    let multiply nums =
        nums
        |> Seq.fold (fun acc current -> acc * current) 1

    let getProducts numDigits (number: string) =

        seq {
            for i = 0 to number.Length - numDigits + 1 do
                number.[i..i + numDigits - 1]
                |> List.ofSeq
                |> Seq.map (fun i -> i |> string |> int)
                |> fun i -> (i |> List.ofSeq, i |> multiply)
        }

    let number =
        (getResource "Problem 8.txt").Replace("\n", "").Replace("\r", "")

    number
    |> getProducts 13
    |> Seq.maxBy (fun (_, product) -> product)


let problem9 () =
    let limit = 1000000

    seq {
        for c = 5 to limit do
            for b = 1 to c do
                for a = 1 to b do
                    if a + b + c = 1000 && isPythagoreanTriplet a b c
                    then ([ a; b; c ], a * b * c)
    }
    |> Seq.take 1
    |> Seq.head


let problem10 () =
    generateNPrimes 1000000000
    |> Seq.takeWhile (fun i -> i < 2000000L)
    |> Seq.sum


let problem12 () =
    Seq.initInfinite (fun i -> nthTriangularNumber i)
    |> Seq.tail
    |> Seq.find (fun i -> (i |> int64 |> factors |> List.ofSeq).Length > 500)


let problem13 () =
    getResource "Problem 13.txt"
    |> fun str -> str.Split()
    |> Seq.filter (fun i -> i <> "")
    |> Seq.map (bigint.Parse)
    |> Seq.sum
    |> string
    |> fun str -> str.[0..9]


let problem14 () =
    [ 1L .. 1000000L ]
    |> Seq.map (fun i -> (i, collatz i))
    |> Seq.maxBy (fun (_, chainCount) -> chainCount)
    |> fun (start, chainCount) -> start


let problem16 () =
    bigint.Pow(bigint.Parse("2"), 1000)
    |> string
    |> sumOfDigits


let problem20 () =
    100
    |> bigint
    |> factorial
    |> string
    |> sumOfDigits


let problem21 () =
    let d n = properDivisors n |> Seq.sum

    let range = [ 2L .. 9999L ]
    let mutable pairs = [] |> seq

    for a in range do
        let da = d a
        let db = d da
        if a = db && a <> da then pairs <- Seq.append pairs [ da; db ]

    pairs |> set |> Seq.sum


let problem22 () =
    let getScore (name: string) index =
        (name |> Seq.map (fun c -> (int c) - 64) |> Seq.sum)
        * (index + 1)

    getResource "Problem 22.txt"
    |> fun str -> str.Split(',')
    |> Seq.map (fun str -> str.Replace("\"", ""))
    |> Seq.sort
    |> Seq.mapi (fun index name -> getScore name index)
    |> Seq.sum

type Designation =
    | Deficient of int64
    | Abundant of int64
    | Perfect of int64

//let problem23 () =
//    let getDesignation n =
//        match properDivisors n |> Seq.sum with
//        | i when i > n -> Abundant n
//        | i when i < n -> Deficient n
//        | i -> Perfect n

//    let canBeWritten n =
//        let range = [ 1 .. n - 1 ]

//        for i in range do
//            ()

//    [ 1L .. 28123L ]
//    |> Seq.map getDesignation
//    |> Seq.choose (function | )


let problem24 () =

    "0123456789"
    |> List.ofSeq
    |> permutations
    |> Seq.map (fun i -> i |> Seq.map string |> String.concat "")
    |> Seq.sort
    |> Seq.item 999999

let problem25 () =
    let digitCount n = n |> string |> String.length

    ()
    |> fib
    |> Seq.findIndex (fun i -> digitCount i = 1000)
    |> (+) 2


let problem29 () =
    let range = [ 2 .. 100 ]

    seq {
        for a in range do
            for b in range do
                yield bigint.Pow(bigint a, b)
    }
    |> Seq.distinct
    |> Seq.sort
    |> Seq.length


let problem30 () =
    let poweredSum pow digits =
        digits
        |> Seq.map (fun i -> Math.Pow(float i, float pow))
        |> Seq.map int64
        |> Seq.sum

    Seq.initInfinite (fun i -> i + 2)
    |> Seq.map string
    |> Seq.filter (fun i -> i |> getDigits |> poweredSum 5 = int64 i)
    |> Seq.take 6
    |> Seq.map int64
    |> Seq.sum


let problem34 () =
    let sumOfFactorialDigits n =
        getDigits n
        |> Seq.map bigint
        |> Seq.map factorial
        |> Seq.sum

    Seq.initInfinite (fun i -> i + 3)
    |> Seq.map bigint
    |> Seq.filter (fun i -> i |> string |> sumOfFactorialDigits = i)
    |> Seq.take 2
    |> Seq.sum


let problem35 () =
    let isCircularPrime n =
        circularPermutations n |> Seq.forall isPrime

    [ 1 .. 1000000 ]
    |> Seq.map string
    |> Seq.filter isCircularPrime
    |> Seq.length


// Not working
let problem36 () =
    [ 1 .. 999999 ]
    |> Seq.map bigint
    |> Seq.filter isPalindrome
    |> Seq.map int64
    |> Seq.map (fun i -> Convert.ToString(i, 2))
    |> Seq.map bigint.Parse
    |> Seq.filter isPalindrome
    |> Seq.map (fun i -> Convert.ToInt32(i.ToString(), 2))
    |> Seq.sum


let problem37 () =
    let isTruncatablePrime p =
        truncatations p true
        |> Seq.map int64
        |> Seq.forall isPrime
        && truncatations p false
           |> Seq.map int64
           |> Seq.forall isPrime

    Seq.initInfinite (fun i -> i + 10)
    |> Seq.filter isTruncatablePrime
    |> Seq.take 11
    |> Seq.sum


let problem40 () =
    let limit = 1000001

    let decimal =
        Seq.initInfinite (fun i -> i + 1)
        |> Seq.takeWhile (fun i -> i < limit)
        |> Seq.map string
        |> String.concat ""

    let d n = decimal.[n - 1] |> string |> int

    Seq.initInfinite (fun i -> Math.Pow(10.0, float i) |> int)
    |> Seq.take 7
    |> Seq.map d
    |> Seq.reduce (fun acc elem -> acc * elem)

let problem42 () =
    let getScore (name: string) =
        (name |> Seq.map (fun c -> (int c) - 64) |> Seq.sum)

    let triangles =
        [ 1 .. 100 ] |> Seq.map nthTriangularNumber

    let words =
        getResource "Problem 42.txt"
        |> fun str -> str.Split(',')
        |> Seq.map (fun str -> str.Replace("\"", ""))

    words
    |> Seq.map getScore
    |> Seq.filter (fun score -> triangles |> Seq.contains score)
    |> Seq.length

let problem48 () =
    [ 1 .. 1000 ]
    |> Seq.map (fun i -> bigint.Pow(bigint i, i))
    |> Seq.sum
    |> string
    |> fun i -> i.[i.Length - 10..i.Length - 1]

let problem49 () =
    let getChain n =
        seq {
            let mutable num = n
            yield num

            while num < 10000 do
                num <- num + 3330
                if num < 10000 then yield num
        }

    let checkChain (chain: int seq) =
        chain
        |> Seq.length = 3
        && chain |> Seq.map int64 |> Seq.forall isPrime
        && chain |> arePermutations


    [ 1000 .. 9999 ]
    |> Seq.map getChain
    |> Seq.filter checkChain
    |> Seq.last
    |> Seq.map string
    |> String.concat ""


let problem52 () =
    let getChain n = [ 1; 2; 3; 4; 5; 6 ] |> Seq.map ((*) n)
    Seq.initInfinite (fun i -> i + 1)
    |> Seq.map getChain
    |> Seq.filter arePermutations
    |> Seq.head
    |> Seq.head

let problem53 () =
    seq {
        for n in [ 1 .. 100 ] |> Seq.map bigint do
            for r in [ bigint 1 .. n ] do
                if choose n r > bigint 1000000 then n
    }
    |> Seq.length


let problem56 () =
    let digitalSum (n: bigint) = n |> string |> getDigits |> Seq.sum

    seq {
        let range = [ 1 .. 100 ]
        for a in range do
            for b in range do
                yield bigint.Pow(bigint a, b)
    }
    |> Seq.map digitalSum
    |> Seq.max


[<EntryPoint>]
let main argv =
    printfn "%A" <| problem56 ()
    0 // return an integer exit code
