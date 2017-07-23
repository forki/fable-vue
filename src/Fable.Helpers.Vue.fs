module Fable.Helpers.Vue

open Fable.Import
open Fable.Core.JsInterop

let private toPojo (o: obj) =
    let o2 = obj()
    for k in JS.Object.getOwnPropertyNames o do
        o2?(k) <- o?(k)
    o2

let initVue: obj = importDefault "vue"

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
