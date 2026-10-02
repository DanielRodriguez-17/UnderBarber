# Spec Delta

## MODIFIED Requirements

### Requirement: RF-01 Crear servicio por tipo
El sistema DEBE crear el servicio base según el tipo indicado ("corte",
"barba", "combo" o "cejas") sin que el código cliente use `new` sobre las
clases concretas.

#### Scenario: Tipo válido
- **WHEN** se pide el servicio "corte"
- **THEN** se obtiene un servicio con descripción "Corte de cabello" y precio 20000

#### Scenario: Tipo válido adicional
- **WHEN** se pide el servicio "cejas"
- **THEN** se obtiene un servicio con descripción "Diseño de cejas" y precio 10000

#### Scenario: Tipo inexistente
- **WHEN** se pide un tipo que no existe
- **THEN** el sistema DEBE lanzar `ArgumentException` con el mensaje "Servicio no existe"

#### Scenario: Variante no registrada
- **WHEN** se pide el tipo "ceja" en singular o "CEJAS" en mayúsculas
- **THEN** el sistema DEBE lanzar `ArgumentException` con el mensaje "Servicio no existe",
  porque la fábrica no normaliza ni hace coincidencia parcial

### Requirement: RF-02 Precios base
Corte = 20000, Barba = 15000, Combo (Corte + Barba) = 30000, Cejas = 10000.

#### Scenario: Precios base definidos
- **WHEN** se crean los servicios base "corte", "barba" y "combo"
- **THEN** sus precios son 20000, 15000 y 30000 respectivamente

#### Scenario: Precios de todos los servicios base
- **WHEN** se crea cada uno de los tipos registrados ("corte", "barba", "combo", "cejas")
- **THEN** sus precios son respectivamente 20000, 15000, 30000 y 10000

## ADDED Requirements

### Requirement: RF-06 Servicio Cejas
El sistema DEBE ofrecer "cejas" como servicio base del catálogo, con descripción
"Diseño de cejas" y precio 10000, alcanzable por el mismo punto de entrada que
los demás servicios.

#### Scenario: Cita de cejas sin extras
- **GIVEN** un cliente con teléfono registrado
- **WHEN** se agenda una cita con tipo "cejas", lavado = no y tinte = no
- **THEN** el resumen muestra "Diseño de cejas" y un precio total de 10000, y se
  envía la confirmación por SMS al teléfono del cliente

#### Scenario: Cita de cejas con extras
- **WHEN** se agenda una cita con tipo "cejas", lavado = sí y tinte = sí
- **THEN** la descripción es "Diseño de cejas + Lavado + Tinte" y el precio total
  es 40000, con el mismo orden de extras que en los demás servicios

#### Scenario: Cita de cejas con un solo extra
- **WHEN** se agenda una cita con tipo "cejas" y solo lavado
- **THEN** la descripción es "Diseño de cejas + Lavado" y el precio total es 15000

#### Scenario: El alta no altera los servicios existentes
- **WHEN** se agendan citas de "corte", "barba" y "combo" después de que exista
  el servicio "cejas"
- **THEN** sus descripciones, precios y notificación se mantienen sin cambios

## Criterios de aceptación

Se ejecuta `dotnet run --project src/UnderBarber` y:

| # | Entrada | Resultado esperado |
|---|---|---|
| CA-6 | Ana, "cejas", lavado=no, tinte=no | "Diseño de cejas", total $10000, SMS enviado al número registrado |
| CA-7 | Ana, "cejas", lavado=sí, tinte=sí | "Diseño de cejas + Lavado + Tinte", total $40000, SMS enviado |
| CA-8 | Tipo "ceja" (singular) | Se lanza `ArgumentException("Servicio no existe")` |
| CA-9 | CA-1 a CA-3 (Juan, Pedro, Luis) | Resultados idénticos a los ya documentados, sin regresión |
| CA-10 | Alta del servicio | Solo se agregan `Cejas.cs` y una línea en `ServicioFactory.Crear`; `BarberiaFacade`, los decoradores y el adaptador SMS quedan intactos |
