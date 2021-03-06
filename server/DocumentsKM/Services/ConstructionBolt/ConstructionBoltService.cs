using System.Collections.Generic;
using DocumentsKM.Models;
using DocumentsKM.Data;
using System;
using DocumentsKM.Dtos;

namespace DocumentsKM.Services
{
    public class ConstructionBoltService : IConstructionBoltService
    {
        private readonly IConstructionBoltRepo _repository;
        private readonly IMarkRepo _markRepo;
        private readonly IConstructionRepo _constructionRepo;
        private readonly IBoltDiameterRepo _boltDiameterRepo;

        public ConstructionBoltService(
            IConstructionBoltRepo constructionBoltRepo,
            IMarkRepo markRepo,
            IConstructionRepo constructionRepo,
            IBoltDiameterRepo boltDiameterRepo)
        {
            _repository = constructionBoltRepo;
            _markRepo = markRepo;
            _constructionRepo = constructionRepo;
            _boltDiameterRepo = boltDiameterRepo;
        }

        public IEnumerable<ConstructionBolt> GetAllByConstructionId(
            int constructionId)
        {
            return _repository.GetAllByConstructionId(constructionId);
        }

        public void Create(
            ConstructionBolt constructionBolt,
            int constructionId,
            int boltDiameterId)
        {
            if (constructionBolt == null)
                throw new ArgumentNullException(nameof(constructionBolt));
            var foundConstruction = _constructionRepo.GetById(constructionId);
            if (foundConstruction == null)
                throw new ArgumentNullException(nameof(foundConstruction));
            var foundBoltDiameter = _boltDiameterRepo.GetById(boltDiameterId);
            if (foundBoltDiameter == null)
                throw new ArgumentNullException(nameof(foundBoltDiameter));

            var uniqueConstraintViolationCheck = _repository.GetByUniqueKey(constructionId, boltDiameterId);
            if (uniqueConstraintViolationCheck != null)
                throw new ConflictException(nameof(uniqueConstraintViolationCheck));

            constructionBolt.Construction = foundConstruction;
            constructionBolt.Diameter = foundBoltDiameter;

            _repository.Add(constructionBolt);

            var foundMark = _markRepo.GetById(foundConstruction.Specification.Mark.Id);
            foundMark.EditedDate = DateTime.Now;
            _markRepo.Update(foundMark);
        }

        public void Update(int id, ConstructionBoltUpdateRequest constructionBoltRequest)
        {
            if (constructionBoltRequest == null)
                throw new ArgumentNullException(nameof(constructionBoltRequest));
            var foundConstructionBolt = _repository.GetById(id);
            if (foundConstructionBolt == null)
                throw new ArgumentNullException(nameof(foundConstructionBolt));

            if (constructionBoltRequest.DiameterId != null)
            {
                var boltDiameter = _boltDiameterRepo.GetById(
                    constructionBoltRequest.DiameterId.GetValueOrDefault());
                if (boltDiameter == null)
                    throw new ArgumentNullException(nameof(boltDiameter));
                
                var uniqueConstraintViolationCheck = _repository.GetByUniqueKey(
                    foundConstructionBolt.Construction.Id, constructionBoltRequest.DiameterId.GetValueOrDefault());
                if (uniqueConstraintViolationCheck != null)
                    throw new ConflictException(nameof(uniqueConstraintViolationCheck));
                
                foundConstructionBolt.Diameter = boltDiameter;
            }

            if (constructionBoltRequest.Packet != null)
                foundConstructionBolt.Packet = constructionBoltRequest.Packet.GetValueOrDefault();
            if (constructionBoltRequest.Num != null)
                foundConstructionBolt.Num = constructionBoltRequest.Num.GetValueOrDefault();
            if (constructionBoltRequest.NutNum != null)
                foundConstructionBolt.NutNum = constructionBoltRequest.NutNum.GetValueOrDefault();
            if (constructionBoltRequest.WasherNum != null)
                foundConstructionBolt.WasherNum = constructionBoltRequest.WasherNum.GetValueOrDefault();

            _repository.Update(foundConstructionBolt);

            var foundMark = _markRepo.GetById(foundConstructionBolt.Construction.Specification.Mark.Id);
            foundMark.EditedDate = DateTime.Now;
            _markRepo.Update(foundMark);
        }

        public void Delete(int id)
        {
            var foundConstructionBolt = _repository.GetById(id);
            if (foundConstructionBolt == null)
                throw new ArgumentNullException(nameof(foundConstructionBolt));
            var markId = foundConstructionBolt.Construction.Specification.Mark.Id;
            _repository.Delete(foundConstructionBolt);

            var foundMark = _markRepo.GetById(markId);
            foundMark.EditedDate = DateTime.Now;
            _markRepo.Update(foundMark);
        }
    }
}
