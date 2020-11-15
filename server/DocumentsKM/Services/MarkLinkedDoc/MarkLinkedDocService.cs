using System.Collections.Generic;
using DocumentsKM.Models;
using DocumentsKM.Data;
using System;
using DocumentsKM.Dtos;

namespace DocumentsKM.Services
{
    public class MarkLinkedDocService : IMarkLinkedDocService
    {
        private IMarkLinkedDocRepo _repository;
        private readonly IMarkRepo _markRepo;
        private readonly ILinkedDocRepo _linkedDocRepo;

        public MarkLinkedDocService(
            IMarkLinkedDocRepo markLinkedDocRepo,
            IMarkRepo markRepo,
            ILinkedDocRepo linkedDocRepo)
        {
            _repository = markLinkedDocRepo;
            _markRepo = markRepo;
            _linkedDocRepo = linkedDocRepo;
        }

        public IEnumerable<MarkLinkedDoc> GetAllByMarkId(int markId)
        {
            return _repository.GetAllByMarkId(markId);
        }

        public void Add(
            MarkLinkedDoc markLinkedDoc,
            int markId,
            int linkedDocId)
        {
            var foundLinkedDoc = _linkedDocRepo.GetById(linkedDocId);
            if (foundLinkedDoc == null)
                throw new ArgumentNullException(nameof(foundLinkedDoc));
            var foundMark = _markRepo.GetById(markId);
            if (foundMark == null)
                throw new ArgumentNullException(nameof(foundMark));

            var uniqueConstraintViolationCheck = _repository.GetByMarkIdAndLinkedDocId(markId, linkedDocId);
            if (uniqueConstraintViolationCheck != null)
                throw new ConflictException(nameof(uniqueConstraintViolationCheck));
            
            markLinkedDoc.Mark = foundMark;
            markLinkedDoc.LinkedDoc = foundLinkedDoc;

            _repository.Add(markLinkedDoc);
        }

        public void Update(int id, MarkLinkedDocRequest markLinkedDocRequest)
        {
            var foundMarkLinkedDoc = _repository.GetById(id);
            if (foundMarkLinkedDoc == null)
                throw new ArgumentNullException(nameof(foundMarkLinkedDoc));

            var foundLinkedDoc = _linkedDocRepo.GetById(markLinkedDocRequest.LinkedDocId);
            if (foundLinkedDoc == null)
                throw new ArgumentNullException(nameof(foundLinkedDoc));
                
            var uniqueConstraintViolationCheck = _repository.GetByMarkIdAndLinkedDocId(
                foundMarkLinkedDoc.Mark.Id, markLinkedDocRequest.LinkedDocId);
            if (uniqueConstraintViolationCheck != null)
                throw new ConflictException(nameof(uniqueConstraintViolationCheck));

            foundMarkLinkedDoc.LinkedDoc = foundLinkedDoc;

            _repository.Update(foundMarkLinkedDoc);
        }

        // public void Delete(
        //     int markId,
        //     int linkedDocId)
        // {
        //     var foundMarkLinkedDoc = _repository.GetByMarkIdAndLinkedDocId(markId, linkedDocId);
        //     if (foundMarkLinkedDoc == null)
        //         throw new ArgumentNullException(nameof(foundMarkLinkedDoc));
        //     _repository.Delete(foundMarkLinkedDoc);
        // }

        public void Delete(int id)
        {
            var foundMarkLinkedDoc = _repository.GetById(id);
            if (foundMarkLinkedDoc == null)
                throw new ArgumentNullException(nameof(foundMarkLinkedDoc));
            _repository.Delete(foundMarkLinkedDoc);
        }
    }
}
