using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BloodBank.Data;
using BloodBank.Models;
using Microsoft.AspNetCore.Identity;
using BloodBank.ViewModels;

namespace BloodBank.Controllers
{
    public class ConversationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<BloodBankUser> _userManager;

        public ConversationsController(ApplicationDbContext context, UserManager<BloodBankUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Conversations
        public async Task<IActionResult> Index()
        {
            return View(await _context.Conversations.ToListAsync());
        }

        // GET: Conversations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conversation = await _context.Conversations
                .FirstOrDefaultAsync(m => m.ConversationId == id);
            if (conversation == null)
            {
                return NotFound();
            }

            return View(conversation);
        }

        public IActionResult Show()
        {
            var currentUser = GetCurrentUser();

            if (currentUser == null)
            {
                return View();
            }

            var conversations = _context
                .Conversations
                .Include("Messages")
                .Where(c => c.PostOwnerName == User.Identity.Name);

            return View(conversations);
        }

        public IActionResult Donate(string postOwnerName)
        {
            string ApplierName = User.Identity.Name;
            string PostOwnerName = postOwnerName;
            
            return RedirectToAction("Contact", new { postOwnerName = PostOwnerName, applierName = ApplierName });
        }

        public IActionResult ShowDialogue(string applierName)
        {
            string ApplierName = applierName;
            string PostOwnerName = User.Identity.Name;

            object routeValues = new { postOwnerName = PostOwnerName, applierName = ApplierName };

            return RedirectToAction("Contact", routeValues);
        }

        private List<Message> GetMessages(Conversation conversation)
        {
            var messageIds = conversation.Messages.Select(c => c.MessageId);

            return _context.Messages.Where(m => messageIds.Contains(m.MessageId)).ToList();
        }
        
        public IActionResult Contact(string postOwnerName, string applierName)
        {
            Conversation conversation = _context
                .Conversations
                .Include(c => c.Messages)
                .Where(c => c.ApplierName == applierName)
                .Where(c => c.PostOwnerName == postOwnerName)
                .FirstOrDefault();

            if (conversation == null)
            {
                Conversation newConversation = new Conversation
                {
                    PostOwnerName = postOwnerName,
                    ApplierName = applierName,
                    Messages = new List<Message>()
                };
                
                ContactViewModel contactViewModel = new ContactViewModel()
                {
                    Conversation = newConversation,
                    Messages = new List<Message>(),
                    OtherPerson = User.Identity.Name == applierName ? postOwnerName : applierName
                };

                _context.Conversations.Add(newConversation);
                _context.SaveChanges();

                return View(contactViewModel);
            }

            List<Message> messages = GetMessages(conversation);

            ContactViewModel cvm = new ContactViewModel()
            {
                Conversation = conversation,
                Messages = messages,
                OtherPerson = User.Identity.Name == applierName ? postOwnerName : applierName
            };

            return View(cvm);
        }

        public IActionResult SendMessage(string PostOwnerName, string ApplierName, string Content)
        {
            var conversation = _context
                .Conversations
                .Include("Messages")
                .Where(c => c.PostOwnerName == PostOwnerName)
                .Where(c => c.ApplierName == ApplierName)
                .FirstOrDefault();

            var message = new Message
            {
                Content = Content,
                OwnerName = User.Identity.Name,
                DateCreated = DateTime.Now
            };

            _context.Messages.Add(message);
            conversation.Messages.Add(message);
            _context.Conversations.Update(conversation);
            _context.SaveChanges();

            object routeValues = new { postOwnerName = PostOwnerName, applierName = ApplierName };

            return RedirectToAction("Contact", routeValues);
        }

        private BloodBankUser GetCurrentUser()
        {
            if (User == null) return null;

            var userName = User.Identity.Name;
            var _currentUser = userName == null ? null : _userManager.FindByNameAsync(User.Identity.Name).Result;

            return _currentUser;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ConversationId,FirstUserName,SecondUserName")] Conversation conversation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(conversation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(conversation);
        }

        // GET: Conversations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conversation = await _context.Conversations.FindAsync(id);
            if (conversation == null)
            {
                return NotFound();
            }
            return View(conversation);
        }

        // POST: Conversations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ConversationId,FirstUserName,SecondUserName")] Conversation conversation)
        {
            if (id != conversation.ConversationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(conversation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConversationExists(conversation.ConversationId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(conversation);
        }

        // GET: Conversations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conversation = await _context.Conversations
                .FirstOrDefaultAsync(m => m.ConversationId == id);
            if (conversation == null)
            {
                return NotFound();
            }

            return View(conversation);
        }

        // POST: Conversations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var conversation = await _context.Conversations.FindAsync(id);
            _context.Conversations.Remove(conversation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConversationExists(int id)
        {
            return _context.Conversations.Any(e => e.ConversationId == id);
        }
    }
}
