using System.Collections.Generic;
using UnityEngine;

public class TransferManager : MonoBehaviour
{
    public class Transfer
    {
        public Transfer(IContainCards sender, CardController card)
        {
            this.sender = sender;
            this.card = card;
            receiver = null;
        }

        public readonly IContainCards sender;
        public readonly CardController card;

        public IContainCards receiver;
    }

    public List<Transfer> Transfers { get; protected set; }

    private void Awake()
    {
        Transfers = new List<Transfer>();
    }

    #region Public methods
    public void InstantTransfer(IContainCards sender, CardController card, IContainCards receiver)
    {
        sender.RemoveCard(card);
        receiver.AddCard(card);
    }

    public void OpenTransfer(IContainCards sender, CardController card)
    {
        sender.RemoveCard(card);

        card.Interaction.OnMouseUpOccur += CloseTransfer;
        card.Collision.OnTriggerEnterOccur += AddReceiver;
        card.Collision.OnTriggerExitOccur += RemoveReceiver;

        Transfers.Add(new Transfer(sender, card));
    }

    public void CloseTransfer(CardController card)
    {
        Transfer transfer = Transfers.Find((t) => t.card.Equals(card));

        if (transfer.receiver == null)
            transfer.sender.AddCard(card);
        else
            transfer.receiver.AddCard(card);

        card.Interaction.OnMouseUpOccur -= CloseTransfer;
        card.Collision.OnTriggerEnterOccur -= AddReceiver;
        card.Collision.OnTriggerExitOccur -= RemoveReceiver;

        Transfers.Remove(transfer);
    }

    public void AddReceiver(IContainCards receiver, CardController card)
    {
        Transfer transfer = Transfers.Find((t) => t.card.Equals(card));

        if (transfer.sender != transfer.receiver)
            transfer.receiver = receiver;
    }

    public void RemoveReceiver(IContainCards receiver, CardController card)
    {
        Transfer transfer = Transfers.Find((t) => t.card.Equals(card));

        transfer.receiver = null;
    }
    #endregion
}
