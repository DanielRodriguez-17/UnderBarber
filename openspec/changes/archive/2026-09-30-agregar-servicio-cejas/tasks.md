# Tareas: Agregar el servicio Cejas

## 1. Servicio y fábrica

- [x] 1.1 Crear `src/UnderBarber/Servicios/Cejas.cs` con la clase `Cejas : IServicio`, `Descripcion => "Diseño de cejas"` y `Precio => 10000`, en el mismo estilo de una línea de `Corte.cs`, y verificar que el archivo compila con `dotnet build src/UnderBarber`
- [x] 1.2 Agregar la rama `"cejas" => new Cejas(),` al final del `switch` de `ServicioFactory.Crear` sin reordenar las ramas existentes, y verificar que `dotnet build src/UnderBarber` sigue sin errores
- [x] 1.3 Confirmar que el caso por defecto no cambia: un tipo desconocido, la variante en singular `"ceja"` y `"CEJAS"` deben seguir lanzando `ArgumentException("Servicio no existe")` (CA-8)

## 2. Demostración en el flujo de citas

- [x] 2.1 Agregar en `src/UnderBarber/Program.cs` una llamada `barberia.AgendarCita("Ana", "<telefono>", "cejas", false, false)` después de las tres existentes, usando solo `BarberiaFacade` y sin ninguna construcción directa de `Cejas`, y verificar con `dotnet run --project src/UnderBarber` que la salida muestra `Servicio: Diseño de cejas` y `Precio total: $10000` (CA-6)
- [x] 2.2 Ejecutar `dotnet run --project src/UnderBarber` y comparar la salida de Juan, Pedro y Luis contra la tabla ya documentada, confirmando que no hay regresión en ninguno de los tres casos (CA-9)
- [x] 2.3 Cambiar temporalmente los extras de la llamada de cejas a `true, true` y verificar que la salida da `Diseño de cejas + Lavado + Tinte` y `Precio total: $40000`; después probar solo lavado y verificar `$15000`; luego dejar el caso en `false, false` como estaba (CA-7)

## 3. Documentación

- [x] 3.1 Actualizar `README.md` para listar `Cejas.cs` en la estructura de `Servicios/`, agregar `cejas` al catálogo de servicios base con su precio de 10000 en el enunciado del problema, y verificar que ninguna otra sección del README queda contradictoria
- [x] 3.2 Agregar el bloque del caso de cejas a la salida esperada del `README.md` y verificar que coincide literalmente con la salida real de `dotnet run --project src/UnderBarber`

## 4. Comprobación final de no regresión

- [x] 4.1 Confirmar que el diff de implementación solo toca `Servicios/Cejas.cs` (nuevo), `Servicios/ServicioFactory.cs` (una línea), `Program.cs` (una llamada) y `README.md`, y que `BarberiaFacade.cs`, `ServicioDecorator.cs`, `ConLavado.cs`, `ConTinte.cs`, `IServicio.cs`, `INotificador.cs`, `AdaptadorSMS.cs` y `SistemaSMSAntiguo.cs` quedan sin modificar (CA-10)
- [x] 4.2 Ejecutar `openspec validate agregar-servicio-cejas` y revisar que no aparezcan errores; antes de archivar este cambio, confirmar que `agendar-cita` ya fue sincronizado o archivado para que las secciones MODIFIED de RF-01 y RF-02 tengan una especificación base
  - `validate`: change válido, 0 errores (solo warnings de RFC 2119 en inglés, ya que la spec está en español con "DEBE", y el aviso esperado de MODIFIED sin spec base)
  - Pendiente de tu decisión: `agendar-cita` sigue sin archivar y `openspec list --specs` está vacío, así que `citas` aún no tiene especificación principal. Hay que archivar o sincronizar `agendar-cita` antes que este cambio.
