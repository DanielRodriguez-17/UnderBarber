# UnderBarber — Patrones de diseño (1 creacional + 3 estructurales)

Mini-sistema de agendamiento de citas para una barbería, hecho en C# (.NET 8)
para demostrar cuatro patrones de diseño: **Factory Method**, **Decorator**,
**Adapter** y **Facade**. Se desarrolló con enfoque SDD usando **OpenSpec**
(la especificación está en [`openspec/`](openspec/)).

## Problema

Una barbería necesita agendar citas. Un cliente (usa el sistema la
recepcionista o la app de la barbería) elige un servicio base (corte, barba,
combo o cejas), puede añadir extras (lavado, tinte) y debe recibir un SMS de
confirmación. Sin patrones, el código que agenda la cita tendría que:

- decidir con `if/else` qué clase de servicio construir,
- crear una clase por cada combinación posible (corte+lavado, corte+tinte,
  combo+lavado+tinte...),
- depender directamente de un sistema de SMS antiguo con una interfaz distinta,
- y repetir todo ese proceso en cada lugar donde se agende una cita.

## Patrones usados

| Tipo | Patrón | Clase(s) | Qué resuelve | Ventaja frente a hacerlo directo |
|---|---|---|---|---|
| Creacional | Factory Method | `ServicioFactory` | Crear el servicio correcto según el tipo pedido | El cliente no usa `new Corte()`/`new Barba()`; agregar un servicio nuevo (por ejemplo `cejas`) es una clase más una línea en la fábrica |
| Estructural | Decorator | `ConLavado`, `ConTinte` | Agregar extras al servicio | Evita una clase por cada combinación; los extras se apilan en cualquier orden |
| Estructural | Adapter | `AdaptadorSMS` | Usar `SistemaSMSAntiguo` con la interfaz `INotificador` | No se modifica el sistema legado y se puede cambiar de proveedor sin tocar el resto |
| Estructural | Facade | `BarberiaFacade` | Ofrecer un solo punto de entrada `AgendarCita()` | El cliente no conoce fábrica, decoradores ni notificador |

## Estructura

```
UnderBarber/
├── openspec/                 # Especificación SDD (se escribió antes del código)
│   ├── project.md
│   ├── changes/agendar-cita/{proposal,design,tasks}.md + specs/citas/spec.md
│   └── changes/agregar-servicio-cejas/{proposal,design,tasks}.md + specs/citas/spec.md
├── src/UnderBarber/          # Código fuente
│   ├── Servicios/            # IServicio, Corte, Barba, Combo, Cejas, ServicioFactory
│   │   └── (servicios base: corte 20000, barba 15000, combo 30000, cejas 10000)
│   ├── Decoradores/          # ServicioDecorator, ConLavado, ConTinte
│   ├── Notificaciones/       # INotificador, SistemaSMSAntiguo, AdaptadorSMS
│   ├── Facade/               # BarberiaFacade
│   └── Program.cs            # Demo
└── UnderBarber.sln
```

## Cómo ejecutar

Requisitos: [.NET SDK 8](https://dotnet.microsoft.com/download).

```bash
dotnet run --project src/UnderBarber
```

También se puede abrir `UnderBarber.sln` en Visual Studio 2022 y presionar F5.

### Salida esperada

```
Cita agendada para Juan
Servicio: Corte de cabello + Lavado
Precio total: $25000
[SMS antiguo] Enviando a 3001234567: Hola Juan, tu cita fue confirmada: Corte de cabello + Lavado
---------------------------------------
Cita agendada para Pedro
Servicio: Corte + Barba + Tinte
Precio total: $55000
[SMS antiguo] Enviando a 3007654321: Hola Pedro, tu cita fue confirmada: Corte + Barba + Tinte
---------------------------------------
Cita agendada para Luis
Servicio: Arreglo de barba
Precio total: $15000
[SMS antiguo] Enviando a 3009998888: Hola Luis, tu cita fue confirmada: Arreglo de barba
---------------------------------------
Cita agendada para Ana
Servicio: Diseño de cejas
Precio total: $10000
[SMS antiguo] Enviando a 3005554444: Hola Ana, tu cita fue confirmada: Diseño de cejas
---------------------------------------
```
