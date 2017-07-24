module FableVue

open Fable.Core
open Fable.Core.JsInterop
open Fable.Helpers.Vue
open Fable.Import
open System

type AppViewModel() =
    let mutable text = "Hello, World!"

    member __.render(h: CreateElement) =
        div [] [
            h1 [] [ str text ]
            button [
                On (createObj [ "click" ==> (fun () -> text <- "bla!") ])
            ] [ str "Kliki" ]
            span [ DomProps (createObj [ "innerHTML" ==> "&nbsp;" ]) ] []
            button [
                On (createObj [ "click" ==> (fun () -> text <- "Hello, World!") ])
            ] [ str "Reset" ]
        ]

let app = mount(AppViewModel(), obj(), "#app")
