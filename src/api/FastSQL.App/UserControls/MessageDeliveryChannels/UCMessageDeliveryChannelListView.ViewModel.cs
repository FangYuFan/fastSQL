﻿using FastSQL.App.Events;
using FastSQL.App.Interfaces;
using FastSQL.Sync.Core.Models;
using FastSQL.Sync.Core.Repositories;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastSQL.App.UserControls.MessageDeliveryChannels
{
    public class UCMessageDeliveryChannelListViewViewModel : BaseViewModel
    {
        private readonly MessageDeliveryChannelRepository messageDeliveryChannelRepository;
        private readonly IEventAggregator eventAggregator;
        private MessageDeliveryChannelModel _selectedChannel;
        private ObservableCollection<MessageDeliveryChannelModel> _channels;

        public BaseCommand SelectItemCommand => new BaseCommand(o => true, o => {
            var id = Guid.Parse(o.ToString());
            eventAggregator.GetEvent<SelectChannelEvent>().Publish(new SelectChannelEventArgument
            {
                ChannelId = id
            });
        });

        public ObservableCollection<MessageDeliveryChannelModel> Channels
        {
            get
            {
                return _channels;
            }
            set
            {
                _channels = value ?? new ObservableCollection<MessageDeliveryChannelModel>();
                OnPropertyChanged(nameof(Channels));
            }
        }

        public MessageDeliveryChannelModel SelectedChannel
        {
            get { return _selectedChannel; }
            set
            {
                _selectedChannel = value;

                OnPropertyChanged(nameof(SelectedChannel));
            }
        }

        public UCMessageDeliveryChannelListViewViewModel(
            MessageDeliveryChannelRepository messageDeliveryChannelRepository,
            IEventAggregator eventAggregator)
        {
            this.messageDeliveryChannelRepository = messageDeliveryChannelRepository;
            this.eventAggregator = eventAggregator;
            Channels = new ObservableCollection<MessageDeliveryChannelModel>(messageDeliveryChannelRepository.GetAll());
            eventAggregator.GetEvent<RefreshChannelListEvent>().Subscribe(OnRefreshConnections);
            var firstConection = Channels.FirstOrDefault();
            if (firstConection != null)
            {
                eventAggregator.GetEvent<SelectChannelEvent>().Publish(new SelectChannelEventArgument
                {
                    ChannelId = firstConection.Id
                });
            }
        }

        private void OnRefreshConnections(RefreshChannelListEventArgument obj)
        {
            Channels = new ObservableCollection<MessageDeliveryChannelModel>(messageDeliveryChannelRepository.GetAll());
            var selectedId = obj.SelectedChannelId;
            if (obj.SelectedChannelId == Guid.Empty)
            {
                var firstConnection = Channels.FirstOrDefault();
                selectedId = firstConnection?.Id ?? Guid.Empty;
            }
            if (selectedId != Guid.Empty)
            {
                eventAggregator.GetEvent<SelectChannelEvent>().Publish(new SelectChannelEventArgument
                {
                    ChannelId = selectedId
                });
            }
        }

        public void Loaded()
        {
        }
    }
}
