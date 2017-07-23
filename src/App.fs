module FableVue

open Fable.Core
open Fable.Core.JsInterop
open Fable.Helpers.Vue
open Fable.Import
open System

let (~%) = createObj

type Component = { id: string }
type AsyncComponent = { id: string }
type VNode = obj

// #2: VNodeData
// #3: VNodeChildren
type CreateElement = U3<string, Component, AsyncComponent> -> obj -> U2<string, VNode>[] -> VNode

type AppViewModel() =
    let mutable text = "Hello, World!"

    member __.render(h: CreateElement) =
        h !^"div" %[] [|
            !^(h !^"h1" %[] [| !^text |])
            !^(h !^"button" %[ "on" ==> %[ "click" ==> (fun _ -> text <- "bla!") ] ] [| !^"Kliki!" |])
        |]

let extraOpts = %[]
let app = mount(AppViewModel(), extraOpts, "#app")
