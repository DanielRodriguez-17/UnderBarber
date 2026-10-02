# Propuesta: Agregar el servicio Cejas

## Por qué

Hoy la barbería solo ofrece tres servicios base (corte, barba y combo) y el
catálogo está cerrado. El diseño de ejemplo ya resuelve bien la creación de
servicios con `ServicioFactory`, pero agregar un cuarto servicio obliga hoy a
tocar código de la fachada o a hardcodear un `if/else` en el cliente. Esta
extensión sirve para **demostrar que el punto de extensión del Factory Method
está en el lugar correcto**: publicar un servicio nuevo debe ser una clase
nueva más una línea en la fábrica, sin tocar a quien agenda la cita.

Además, `cejas` es el caso que mejor expone la otra ventaja del enunciado: al
ser el servicio más barato, sirve para comprobar que los decoradores de extras
(Lavado +5000, Tinte +25000) se apilan sobre cualquier servicio, no solo sobre
corte o barba.

## Qué cambia

- Se agrega la clase `Cejas` en `UnderBarber.Servicios`, que implementa
  `IServicio` con descripción `"Diseño de cejas"` y precio `10000`.
- Se agrega el caso `"cejas"` al `switch` de `ServicioFactory.Crear`, con lo
  cual el servicio queda disponible para `BarberiaFacade.AgendarCita` y para
  cualquier cliente que use la fábrica.
- Se extiende el catálogo documentado: la lista de tipos válidos y la tabla de
  precios base pasan de 3 a 4 entradas.
- Se agrega un caso de demostración en `Program.cs` y se actualiza el
  `README.md` (estructura, catálogo y salida esperada) para que la salida real
  siga coincidiendo con la documentada.
- No hay cambios en la API pública: `AgendarCita`, `IServicio`, los
  decoradores, el adaptador SMS y la firma de `ServicioFactory.Crear` se
  mantienen igual. No es un cambio incompatible.

### Supuestos registrados

- **Descripción del servicio**: se usa `"Diseño de cejas"`, siguiendo el estilo
  de las descripciones existentes ("Corte de cabello", "Arreglo de barba").
  Es un texto visible en consola y en el SMS, así que es trivial de ajustar
  si se prefiere otra redacción.
- **Clave del tipo**: `"cejas"` en minúsculas, igual que `"corte"`, `"barba"`
  y `"combo"`. `ServicioFactory` no normaliza la entrada, así que `"Cejas"` o
  `"CEJAS"` seguirían lanzando `ArgumentException`.
- **`Combo` no cambia**: sigue siendo "Corte + Barba" a 30000. Agregar cejas al
  combo no es parte de este cambio.
- **Sin servicio de cejas propio en decoradores**: los extras se aplican igual
  que a cualquier otro servicio porque los decoradores trabajan contra
  `IServicio`, no contra tipos concretos.

## Capacidades

### Capacidades nuevas

Ninguna. No se introduce comportamiento de un dominio que antes no existía: se
extiende el catálogo de servicios de la barbería, que ya está descrito.

### Capacidades modificadas

- `citas`: los requisitos de creación de servicio por tipo y de precios base
  cambian — la lista de tipos válidos pasa a incluir `"cejas"` y el precio del
  nuevo servicio es 10000. Se agregan además el escenario de aceptación del
  nuevo servicio y la confirmación de que admite extras.

> **Nota de orden.** La especificación base de la capacidad `citas` todavía no
> está en `openspec/specs/`; vive en el cambio pendiente `agendar-cita`
> (completo, sin archivar). Este delta modifica RF-01 y RF-02 de esa
> especificación, por lo que conviene archivar o sincronizar `agendar-cita`
> **antes** de aplicar y archivar este cambio, para que el delta tenga una base
> sobre la que aplicarse.

## Impacto

**Código afectado (aditivo salvo el `switch`):**

| Archivo | Cambio |
|---|---|
| `src/UnderBarber/Servicios/Cejas.cs` | Nuevo. Clase `Cejas : IServicio` |
| `src/UnderBarber/Servicios/ServicioFactory.cs` | Una línea en el `switch` de `Crear` |
| `src/UnderBarber/Program.cs` | Un caso más de demostración |
| `README.md` | Catálogo, estructura y salida esperada |

**Sin impacto en:** `BarberiaFacade`, `IServicio`, `ServicioDecorator`,
`ConLavado`, `ConTinte`, `INotificador`, `AdaptadorSMS`,
`SistemaSMSAntiguo`, `UnderBarber.csproj` y `UnderBarber.sln`. No se agregan
dependencias ni paquetes.

**Verificación:** `dotnet run --project src/UnderBarber` debe compilar sin
errores y mostrar el nuevo caso con total $10000 sin extras.
