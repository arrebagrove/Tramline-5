﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TramlineFive.DataAccess.Entities;
using TramlineFive.DataAccess.Repositories;

namespace TramlineFive.DataAccess.DomainLogic
{
    public class LineDO
    {
        public LineDO(Line entity)
        {
            id = entity.ID;
            name = WebUtility.UrlDecode(entity.Name);
            directions = entity.Directions?.Select(d => new DirectionDO(d));

            string[] split = Name.Split('/');
            switch (split[0])
            {
                case "tramway":
                    type = "Трамвай";
                    break;
                case "autobus":
                    type = "Автобус";
                    break;
                case "trolleybus":
                    type = "Тролей";
                    break;

                default:
                    type = null;
                    break;
            }

            int tempNum;
            if (Int32.TryParse(split[1], out tempNum))
                number = tempNum;
            else
                number = Int32.Parse(split[1][0].ToString());
        }

        public static async Task<IEnumerable<LineDO>> AllAsync()
        {
            return await Task.Run(() =>
            {
                using (UnitOfWork uow = new UnitOfWork())
                {
                    IEnumerable<Line> lines = uow.Lines.All().ToList();
                    return lines?.Select(l => new LineDO(l));
                }
            });
        }

        public async Task LoadDirections()
        {
            directions = await DirectionDO.GetByLineId(id);
        }

        private int id;

        private string name;
        public string Name
        {
            get
            {
                return name;
            }
        }

        private string type;
        public string Type
        {
            get
            {
                return type;
            }
        }

        private int number;
        public int Number
        {
            get
            {
                return number;
            }
        }

        private IEnumerable<DirectionDO> directions;
        public IEnumerable<DirectionDO> Directions
        {
            get
            {
                return directions;
            }
        }

        public override string ToString()
        {
            string[] split = Name.Split('/');
            string type = String.Empty;

            switch (split[0])
            {
                case "tramway":
                    type = "Трамвай";
                    break;
                case "autobus":
                    type = "Автобус";
                    break;
                case "trolleybus":
                    type = "Тролей";
                    break;

            }

            return $"{type} {split[1]}";
        }
    }
}
