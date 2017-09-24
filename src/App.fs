module FableVue

open Fable.Core
open Fable.Core.JsInterop
open Fable.Helpers.Vue
open Fable.Import
open System

let [<Literal>] ALL_TODOS = "all"

type TodoAppComponent () =
    let mutable nowShowing = ALL_TODOS
    let mutable editing = None
    let mutable newTodo = ""

    member __.mounted () =
        let changeCategory category =
            fun () -> nowShowing <- category
        (*
        let router =
            Router(createObj [
                    "/" ==> nowShowing ALL_TODOS
                    "/active" ==> nowShowing ACTIVE_TODOS
                    "/completed" ==> nowShowing COMPLETED_TODOS
            ])
        router?init("/")
        *)
        ()

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

let app = mount(AppViewModel(), obj(), ".todoapp")
