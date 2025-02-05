using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBack.Aplication.Dtos;
using UBack.Aplication.Interfaces;
using UBack.Domain.Dominio.Entities;
using UBack.Transversal.Common;

namespace UBack.Aplication.UseCases
{   
    public class MateriaUseCases(IMateriaRepository MateriaRepository, IMapper mapper)
    {
        private readonly IMateriaRepository _MateriaRepository = MateriaRepository;
        private readonly IMapper _mapper = mapper;

        // Agregar un nuevo Materia
        public async Task<Response<MateriaDTO>> AddMateriaAsync(MateriaDTO MateriaDto)
        {
            var response = new Response<MateriaDTO>();
            try
            {

                // Mapeo del DTO a modelo de dominio
                var Materia = _mapper.Map<Materia>(MateriaDto);

                // Agregar el Materia en la base de datos
                var result = await _MateriaRepository.AddMateriaAsync(Materia);
                if (result)
                {
                    response.Data = MateriaDto;
                    response.IsSuccess = true;
                    response.Message = "Materia creado exitosamente.";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "No se pudo crear el Materia.";
                }
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = $"Error: {e.Message}";
            }

            return response;
        }

        // Obtener un Materia por ID
        public async Task<MateriaDTO> GetMateriaByIdAsync(int id)
        {
            var Materia = await _MateriaRepository.GetMateriaByIdAsync(id);
            return Materia != null ? _mapper.Map<MateriaDTO>(Materia) : null;
        }

        // Obtener todos los Materias
        public async Task<IEnumerable<MateriaDTO>> GetAllMateriasAsync()
        {
            var Materias = await _MateriaRepository.GetAllMateriasAsync();
            return _mapper.Map<IEnumerable<MateriaDTO>>(Materias);
        }

        // Actualizar un Materia
        public async Task<Response<MateriaDTO>> UpdateMateriaAsync(int id, MateriaDTO MateriaDto)
        {
            var response = new Response<MateriaDTO>();
            try
            {
                var Materia = await _MateriaRepository.GetMateriaByIdAsync(id);
                if (Materia == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Materia no encontrado.";
                    return response;
                }

                // Mapeo del DTO al modelo de dominio
                _mapper.Map(MateriaDto, Materia);

                var result = await _MateriaRepository.UpdateMateriaAsync(Materia);
                if (result)
                {
                    response.Data = MateriaDto;
                    response.IsSuccess = true;
                    response.Message = "Materia actualizado exitosamente.";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "No se pudo actualizar el Materia.";
                }
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = $"Error: {e.Message}";
            }

            return response;
        }

        // Eliminar un Materia
        public async Task<Response<MateriaDTO>> DeleteMateriaAsync(int id)
        {
            var response = new Response<MateriaDTO>();
            try
            {
                var Materia = await _MateriaRepository.GetMateriaByIdAsync(id);
                if (Materia == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Materia no encontrado.";
                    return response;
                }

                var result = await _MateriaRepository.DeleteMateriaAsync(id);
                if (result)
                {
                    response.IsSuccess = true;
                    response.Message = "Materia eliminado exitosamente.";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "No se pudo eliminar el Materia.";
                }
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = $"Error: {e.Message}";
            }

            return response;
        }
    }
}