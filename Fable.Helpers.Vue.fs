module Fable.Helpers.Vue

open Fable.Import
open Fable.Core
open Fable.Core.JsInterop
open System

type VNode = obj
type Component = { id: string }
type AsyncComponent = { id: string }
type CreateElement = U3<string, Component, AsyncComponent> -> obj -> VNode list -> VNode

type VueProp =
    | Class of obj
    | Style of obj
    | Attrs of obj
    | Props of obj
    | DomProps of obj
    | On of obj
    | NativeOn of obj
    | Directives of obj []
    | ScopedSlots of obj
    | Slot of string
    | Key of string
    | Ref of string

[<Emit("h($0, $1, $2)")>]
let createEl (tag: U3<string, Component, AsyncComponent>) (data: obj) (children: VNode []) : VNode = jsNative

let inline domEl (tag: string) (props: VueProp list) (children: VNode list): VNode =
    createEl !^tag (keyValueList CaseRules.LowerFirst props) (children |> Array.ofList)

let inline button b c = domEl "button" b c
let inline div b c = domEl "div" b c
let inline h1 b c = domEl "h1" b c
let inline span b c = domEl "span" b c

let [<Emit("$0")>] str (s: string): VNode = unbox s

let initVue: obj = importDefault "vue"

let private toPojo (o: obj) =
    let o2 = obj()
    for k in JS.Object.getOwnPropertyNames o do
        o2?(k) <- o?(k)
    o2

let mount(data: obj, extraOpts: obj, el: string) =
    let methods = obj()
    let computed = obj()
    let proto = JS.Object.getPrototypeOf data
    for k in JS.Object.getOwnPropertyNames proto do
        let prop = JS.Object.getOwnPropertyDescriptor(proto, k)
        match k, prop.value with
        | "render", Some f -> extraOpts?render <- f
        | _, Some f -> methods?(k) <- f
        | _, None ->
            computed?(k) <- createObj [
                "get" ==> prop?get
                "set" ==> prop?set
            ]
    extraOpts?data <- toPojo data
    extraOpts?computed <- computed
    extraOpts?methods <- methods
    let app = createNew initVue extraOpts
    app?``$mount``(el) |> ignore
    app
